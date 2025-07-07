using Kathanika.Infrastructure.Graphql.Types;

namespace Kathanika.Infrastructure.Graphql.Bases;

public abstract record Payload
{
    protected Payload(KnResult knResult, string? message = null)
    {
        Errors = knResult.Errors?.Length != 0 ? knResult.Errors : null;
        Message = message;
    }

    [GraphQLType<ListType<NonNullType<ErrorType>>>]
    public KnError[]? Errors { get; private init; }

    public string? Message { get; private init; }
}

public abstract record Payload<TData> : Payload
    where TData : class
{
    protected Payload(KnResult<TData> knResult, string? message = null)
        : base(knResult, message)
    {
        if (knResult.IsSuccess)
            Data = knResult.Value;
    }

    public TData? Data { get; private init; }
}