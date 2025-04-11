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
            ).Value
        ));

        KnResult<PurchaseOrder> purchaseOrderResult = PurchaseOrder.Create(
            vendor.Id,
            vendor.Name,
            purchaseItems,
            request.InternalNote,
            request.VendorNote);

        if (purchaseOrderResult.IsFailure)
            return purchaseOrderResult;

        PurchaseOrder createdPurchaseOrder = await purchaseOrderRepository.AddAsync(purchaseOrderResult.Value, cancellationToken);

        return KnResult.Success(createdPurchaseOrder);
    }
}