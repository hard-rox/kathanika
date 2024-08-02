using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record UpdateAuthorPayload
    : Payload<Author>
{
    public UpdateAuthorPayload(Core.Domain.Primitives.Result<Author> result) : base(
        result,
    result.IsSuccess ?
    $"Author {result.Value?.FullName} updated successfully." :
    $"Author update failed."
)
    { }
}
