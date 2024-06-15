using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class AddAuthorPayload(Author data) : Payload<Author>($"New author {data.FullName} added successfully.", data)
{
}
