using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class DeleteAuthorPayload : Payload
{
    public DeleteAuthorPayload(string id) : base($"Author with Id: {id} deleted.")
    {
    }
}
