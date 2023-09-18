using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class UpdatePublisherPayload : Payload<Publisher>
{
    public UpdatePublisherPayload(Publisher data) : base($"Author {data.PublisherName} updated successfully.", data)
    {
    }
}
