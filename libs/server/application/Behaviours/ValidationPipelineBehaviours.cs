using Kathanika.Domain.Exceptions;

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

        InvalidFieldException[] invalidFieldExceptions = validators
            .Select(async validator => await validator.ValidateAsync(request))
            .SelectMany(result => result.Result.Errors)
            .Where(error => error is not null)
            .Select(error => new InvalidFieldException(error.PropertyName, error.ErrorMessage))
            .Distinct()
            .ToArray();
        if (invalidFieldExceptions.Any())
        {
            throw new AggregateException(invalidFieldExceptions);
        }

        return await next();
    }
}
