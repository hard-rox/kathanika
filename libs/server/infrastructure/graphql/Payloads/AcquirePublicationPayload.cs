using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class AcquirePublicationPayload(Publication data) : Payload<Publication>($"New publication {data.Title} added successfully.", data)
{
}
