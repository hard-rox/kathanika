using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class DeleteAuthorPayload(string id) : Payload($"Author with Id: {id} deleted.")
{
}
