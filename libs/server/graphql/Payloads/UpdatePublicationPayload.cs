using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class UpdatePublicationPayload(Publication data) : Payload<Publication>($"Publication {data.Title} updated successfully.", data)
{
}
