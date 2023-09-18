using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class AddPublisherPayload : Payload<Publisher>
{
    public AddPublisherPayload(Publisher data) : base($"New author {data.Name} added successfully.", data)
    {
    }
}
