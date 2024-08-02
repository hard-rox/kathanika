using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record DeletePublisherPayload : Payload
{
    public DeletePublisherPayload(string id, Result result)
        : base(
            result,
            result.IsSuccess ?
            $"Publisher with Id: {id} deleted." :
            $"Publisher deletion failed."
        )
    { }
}
