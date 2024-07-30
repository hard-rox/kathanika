using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record DeletePublisherPayload : Payload
{
    public DeletePublisherPayload(bool isSuccess, string id)
        : base(
            isSuccess ?
            $"Publisher with Id: {id} deleted." :
            $"Publisher deletion failed."
        )
    { }
}
