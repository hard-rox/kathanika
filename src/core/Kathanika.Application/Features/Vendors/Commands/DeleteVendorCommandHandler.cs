using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.Vendors.Commands;

internal sealed class DeleteVendorCommandHandler(
    IVendorRepository vendorRepository,
    IPurchaseOrderRepository purchaseOrderRepository)
    : IRequestHandler<DeleteVendorCommand, KnResult>
{
    public async Task<KnResult> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {
        if (await vendorRepository.GetByIdAsync(request.Id, cancellationToken) is null)
            return VendorAggregateErrors.NotFound(request.Id);

        if (await purchaseOrderRepository.ExistsAsync(
                x => x.VendorId == request.Id && x.Status == PurchaseOrderStatus.Pending, cancellationToken))
            return VendorAggregateErrors.HasActivePurchaseOrders(request.Id);

        await vendorRepository.DeleteAsync(request.Id, cancellationToken);

        return KnResult.Success();
    }
}