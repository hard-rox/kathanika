using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class UpdatePublicationPayload : Payload<Publication>
{
    public UpdatePublicationPayload(Publication data) : base($"Publication {data.Title} updated successfully.", data)
    {
    }
}
