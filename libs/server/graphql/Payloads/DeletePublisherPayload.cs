using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class DeletePublisherPayload : Payload
{
    public DeletePublisherPayload(string id) : base($"Publisher with Id: {id} deleted.")
    {
    }
}
