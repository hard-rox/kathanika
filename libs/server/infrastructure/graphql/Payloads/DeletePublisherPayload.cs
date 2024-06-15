using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class DeletePublisherPayload(string id) : Payload($"Publisher with Id: {id} deleted.")
{
}
