using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class AddPublicationPayload : Payload<Publication>
{
    public AddPublicationPayload(Publication data) : base($"New publication {data.Title} added successfully.", data)
    {
    }
}
