using Kathanika.Infrastructure.Graphql.Types;

namespace Kathanika.Infrastructure.Graphql.Bases;

public abstract record Payload
{
    public Payload(Result result, string? message = null)
    {
        Errors = result.Errors;
        Message = message;
    }

    [GraphQLType<ListType<ErrorType>>]
    public KnError[]? Errors { get; private init; }
    public string? Message { get; private init; }
}

public abstract record Payload<TData> : Payload
    where TData : class
{
    public Payload(Core.Domain.Primitives.Result<TData> result, string? message = null)
        : base(result, message)
    {
        Data = result.Value;
    }

    public TData? Data { get; private init; }
}
