using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class UpdateAuthorPayload : Payload<Author>
{
    public UpdateAuthorPayload(Author data) : base($"Author {data.FullName} updated successfully.", data)
    {
    }
}
