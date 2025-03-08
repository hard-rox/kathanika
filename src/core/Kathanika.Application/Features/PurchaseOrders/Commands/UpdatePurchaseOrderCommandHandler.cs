using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.PurchaseOrders.Commands;

internal sealed class UpdatePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository, IVendorRepository vendorRepository)
    : IRequestHandler<UpdatePurchaseOrderCommand, KnResult<PurchaseOrder>>
{
    public async Task<KnResult<PurchaseOrder>> Handle(UpdatePurchaseOrderCommand request,
        CancellationToken cancellationToken)
    {
        PurchaseOrder? purchaseOrder = await purchaseOrderRepository.GetByIdAsync(request.Id, cancellationToken);
        if (purchaseOrder is null)
            return PurchaseOrderAggregateErrors.NotFound(request.Id);

        Vendor? vendor = null;
        if (!string.IsNullOrWhiteSpace(request.Patch.VendorId) && request.Patch.VendorId != purchaseOrder.VendorId)
        {
            vendor = await vendorRepository.GetByIdAsync(request.Patch.VendorId, cancellationToken);
        }

        KnResult purchaseOrderUpdateResult = purchaseOrder.Update(
            request.Patch.VendorId,
            vendor?.Name,
            request.Patch.InternalNote,
            request.Patch.VendorNote
        );

        if (purchaseOrderUpdateResult.IsFailure)
            return purchaseOrderUpdateResult.Errors;

        await purchaseOrderRepository.UpdateAsync(purchaseOrder, cancellationToken);

        return purchaseOrder;
    }
}