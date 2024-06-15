using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class UpdatePublicationPayload(Publication data) : Payload<Publication>($"Publication {data.Title} updated successfully.", data)
{
}
