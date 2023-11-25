using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class AddAuthorPayload(Author data) : Payload<Author>($"New author {data.FullName} added successfully.", data)
{
}
