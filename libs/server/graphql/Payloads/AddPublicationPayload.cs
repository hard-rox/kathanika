using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class AddPublicationPayload(Publication data) : Payload<Publication>($"New publication {data.Title} added successfully.", data)
{
}
