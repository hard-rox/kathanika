using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record DeleteAuthorPayload
    : Payload
{
    public DeleteAuthorPayload(string id, Result result) : base(
        result,
        result.IsSuccess ?
        $"Author with Id: {id} deleted." :
        $"Author with Id: {id} deletion failed."
    )
    {
    }
}
