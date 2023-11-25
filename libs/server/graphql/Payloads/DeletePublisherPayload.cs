using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class DeletePublisherPayload(string id) : Payload($"Publisher with Id: {id} deleted.")
{
}
