using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record UpdateAuthorPayload
    : Payload<Author>
{
    public UpdateAuthorPayload(Author? Data) : base(
    Data is not null ?
    $"Author {Data.FullName} updated successfully." :
    $"Author update failed.",
    Data
)
    { }
}
