using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class UpdatePublicationPayload(Publication data) : Payload<Publication>($"Publication {data.Title} updated successfully.", data)
{
}
