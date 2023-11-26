using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class AddPublisherPayload(Publisher data) : Payload<Publisher>($"New author {data.Name} added successfully.", data)
{
}
