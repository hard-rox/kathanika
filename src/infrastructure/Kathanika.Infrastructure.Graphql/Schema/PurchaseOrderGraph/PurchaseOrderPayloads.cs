using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.PurchaseOrderGraph;

public sealed record CreatePurchaseOrderPayload : Payload<PurchaseOrder>
{
    public CreatePurchaseOrderPayload(KnResult<PurchaseOrder> knResult) : base(
        knResult,
        knResult.IsSuccess ? $"New purchase order successfully." : "Purchase order creation failed."
    )
    {
    }
}

public sealed record UpdatePurchaseOrderPayload : Payload<PurchaseOrder>
{
    public UpdatePurchaseOrderPayload(KnResult<PurchaseOrder> knResult) : base(
        knResult,
        knResult.IsSuccess ? $"Purchase order updated successfully." : "Purchase order update failed."
    )
    {
    }
}