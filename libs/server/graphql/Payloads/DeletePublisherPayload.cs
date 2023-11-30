using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class DeletePublisherPayload(string id) : Payload($"Publisher with Id: {id} deleted.")
{
}
