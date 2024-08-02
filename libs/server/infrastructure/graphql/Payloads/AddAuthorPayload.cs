using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record AddAuthorPayload
    : Payload<Author>
{
    public AddAuthorPayload(Core.Domain.Primitives.Result<Author> result)
        : base(result, result.IsSuccess ?
    $"New author {result.Value?.FullName} added successfully." :
    $"New author add failed.")
    { }
}
