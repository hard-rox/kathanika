using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

namespace Kathanika.Application.Features.PurchaseOrders.Queries;

internal sealed class GetPurchaseOrdersQueryHandler(IPurchaseOrderRepository vendorRepository)
    : IRequestHandler<GetPurchaseOrdersQuery, IQueryable<PurchaseOrder>>
{
    public async Task<IQueryable<PurchaseOrder>> Handle(GetPurchaseOrdersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<PurchaseOrder> vendorsQuery = await Task.Run(vendorRepository.AsQueryable, cancellationToken);
        return vendorsQuery;
    }
}