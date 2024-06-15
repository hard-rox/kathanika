using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class PurchasePublicationPayload(Publication data)
    : Payload<Publication>($"Saved purchase of publication {data.Title} successfully.", data)
{
}
