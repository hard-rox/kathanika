using System.Reflection;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Behaviours;

public class ValidationPipelineBehaviours<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (!validators.Any())
        {
            return await next();
        }

        KnError[] validationErrors = validators
            .Select(async validator => await validator.ValidateAsync(request, cancellationToken))
            .SelectMany(result => result.Result.Errors)
            .Where(error => error is not null)
            .Select(error => KnError.ValidationError(error.PropertyName, error.ErrorMessage))
            .Distinct()
            .ToArray();

        if (validationErrors.Length == 0)
        {
            return await next();
        }

        Type responseType = typeof(TResponse);
        if (responseType == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(validationErrors);
        }

        if (!responseType.IsGenericType || responseType.GetGenericTypeDefinition() != typeof(Result<>))
        {
            throw new Exception("Invalid response type"); //TODO: More specific...
        }

        Type genericArgument = responseType.GetGenericArguments()[0];
        MethodInfo failureMethod = typeof(Result<>)
                                       .MakeGenericType(genericArgument)
                                       .GetMethod("Failure", [typeof(IEnumerable<KnError>)])
                                   ?? throw new Exception("Result method not found.");

        var resultInstance = failureMethod.Invoke(null, [validationErrors])
                             ?? throw new Exception("Could not create result");
        return (TResponse)resultInstance;
    }
}