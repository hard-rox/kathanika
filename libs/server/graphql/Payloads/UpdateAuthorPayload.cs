using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class UpdateAuthorPayload(Author data) : Payload<Author>($"Author {data.FullName} updated successfully.", data)
{
}
