using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

namespace Kathanika.Application.Features.PurchaseOrders.Queries;

internal sealed class GetPurchaseOrderByIdQueryHandler(IPurchaseOrderRepository vendorRepository)
    : IQueryHandler<GetPurchaseOrderByIdQuery, KnResult<PurchaseOrder>>
{
    public async Task<KnResult<PurchaseOrder>> Handle(GetPurchaseOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        PurchaseOrder? vendor = await vendorRepository.GetByIdAsync(request.Id, cancellationToken);

        return vendor is null
            ? KnResult.Failure<PurchaseOrder>(PurchaseOrderAggregateErrors.NotFound(request.Id))
            : KnResult.Success(vendor);
    }
}