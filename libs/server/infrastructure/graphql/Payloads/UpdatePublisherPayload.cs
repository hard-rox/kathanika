using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class UpdatePublisherPayload(Publisher data) : Payload<Publisher>($"Author {data.Name} updated successfully.", data)
{
}
