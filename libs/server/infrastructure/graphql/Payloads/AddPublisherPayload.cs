using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class AddPublisherPayload(Publisher data) : Payload<Publisher>($"New author {data.Name} added successfully.", data)
{
}
