using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record PurchasePublicationPayload
    : Payload<Publication>
{
    public PurchasePublicationPayload(Core.Domain.Primitives.Result<Publication> result)
    : base(
        result, result.IsSuccess
        ? $"Saved purchase of publication {result.Value?.Title} successfully."
    : "Purchase publication failed.")
    { }
}
