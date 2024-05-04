using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class PurchasePublicationPayload(Publication data)
    : Payload<Publication>($"Saved purchase of publication {data.Title} successfully.", data)
{
}
