using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class AddPublisherPayload(Publisher data) : Payload<Publisher>($"New author {data.Name} added successfully.", data)
{
}
