using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.PurchaseOrders.Commands;

internal sealed class CreatePurchaseOrderCommandHandler(
    ILogger<CreatePurchaseOrderCommandHandler> logger,
    IVendorRepository vendorRepository,
    IPurchaseOrderRepository purchaseOrderRepository)
    : IRequestHandler<CreatePurchaseOrderCommand, KnResult<PurchaseOrder>>
{
    public async Task<KnResult<PurchaseOrder>> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        Vendor? vendor = await vendorRepository.GetByIdAsync(request.VendorId, cancellationToken);
        if (vendor is null)
            return KnResult<PurchaseOrder>.Failure(VendorAggregateErrors.NotFound(request.VendorId));

        List<PurchaseItem> purchaseItems = [];
        purchaseItems.AddRange(request.Items.Select(purchaseItemDto =>
            PurchaseItem.Create(
                purchaseItemDto.Title,
                purchaseItemDto.Quantity,
                purchaseItemDto.Author,
                purchaseItemDto.Publisher,
                purchaseItemDto.Edition,
                purchaseItemDto.PublishingYear,
                purchaseItemDto.Isbn,
                purchaseItemDto.VendorPrice,
                purchaseItemDto.InternalNote,
                purchaseItemDto.VendorNote
            )
        ));

        PurchaseOrder purchaseOrder = PurchaseOrder.Create(
            request.VendorId,
            vendor.Name,
            purchaseItems,
            request.InternalNote,
            request.VendorNote);

        await purchaseOrderRepository.AddAsync(purchaseOrder, cancellationToken);

        return KnResult.Success(purchaseOrder);
    }
}