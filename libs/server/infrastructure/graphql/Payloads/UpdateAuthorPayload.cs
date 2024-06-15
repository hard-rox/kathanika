using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class UpdateAuthorPayload(Author data) : Payload<Author>($"Author {data.FullName} updated successfully.", data)
{
}
