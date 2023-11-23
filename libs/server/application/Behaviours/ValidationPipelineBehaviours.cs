using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Behaviours;

public class ValidationPipelineBehaviours<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehaviours(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        if (!_validators.Any())
        {
            return await next();
        }

        InvalidFieldException[] invalidFieldExceptions = _validators
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
