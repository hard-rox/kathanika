using Kathanika.Infrastructure.Graphql.Types;

namespace Kathanika.Infrastructure.Graphql.Bases;

public abstract record Payload
{
    protected Payload(Result result, string? message = null)
    {
        Errors = result.Errors.Length != 0 ? result.Errors : null;
        Message = message;
    }

    [GraphQLType<ListType<NonNullType<ErrorType>>>]
    public KnError[]? Errors { get; private init; }
    public string? Message { get; private init; }
}

public abstract record Payload<TData> : Payload
    where TData : class
{
    protected Payload(Core.Domain.Primitives.Result<TData> result, string? message = null)
        : base(result, message)
    {
        if (result.IsSuccess)
            Data = result.Value;
    }

    public TData? Data { get; private init; }
}
