using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class AddAuthorPayload(Author data) : Payload<Author>($"New author {data.FullName} added successfully.", data)
{
}
