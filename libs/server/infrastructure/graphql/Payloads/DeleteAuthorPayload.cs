using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class DeleteAuthorPayload(string id) : Payload($"Author with Id: {id} deleted.")
{
}
