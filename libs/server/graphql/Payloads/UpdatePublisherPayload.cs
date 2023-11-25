using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class UpdatePublisherPayload(Publisher data) : Payload<Publisher>($"Author {data.Name} updated successfully.", data)
{
}
