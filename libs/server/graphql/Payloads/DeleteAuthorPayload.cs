using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class DeleteAuthorPayload(string id) : Payload($"Author with Id: {id} deleted.")
{
}
