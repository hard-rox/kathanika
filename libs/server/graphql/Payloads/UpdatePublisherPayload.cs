using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class UpdatePublisherPayload(Publisher data) : Payload<Publisher>($"Author {data.Name} updated successfully.", data)
{
}
