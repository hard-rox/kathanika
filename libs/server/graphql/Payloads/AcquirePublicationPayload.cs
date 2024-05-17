using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class AcquirePublicationPayload(Publication data) : Payload<Publication>($"New publication {data.Title} added successfully.", data)
{
}
