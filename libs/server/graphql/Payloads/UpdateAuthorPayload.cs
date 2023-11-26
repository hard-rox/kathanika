using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class UpdateAuthorPayload(Author data) : Payload<Author>($"Author {data.FullName} updated successfully.", data)
{
}
