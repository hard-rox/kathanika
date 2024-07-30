using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record AddAuthorPayload
    : Payload<Author>
{
    public AddAuthorPayload(Author? Data) : base(
    Data is not null ?
    $"New author {Data.FullName} added successfully." :
    $"New author add failed.",
    Data
)
    { }
}
