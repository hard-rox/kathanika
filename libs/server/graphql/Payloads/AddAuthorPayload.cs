using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class AddAuthorPayload : Payload<Author>
{
    public AddAuthorPayload(Author data) : base($"New author {data.FullName} added successfully.", data)
    {
    }
}
