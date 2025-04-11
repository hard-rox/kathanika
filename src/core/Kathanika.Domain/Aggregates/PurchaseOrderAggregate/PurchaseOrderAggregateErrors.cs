using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

public static class PurchaseOrderAggregateErrors
{
    public static KnError NotFound(string id)
    {
        return new KnError(
            "PurchaseOrder.NotFound",
            $"No purchase order found with this Id: \"{id}\""
        );
    }
}