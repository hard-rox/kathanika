using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

namespace Kathanika.Application.Features.PurchaseOrders.Commands;

public sealed record CreatePurchaseOrderCommand(
    string VendorId,
    PurchaseItemDto[] Items,
    string? InternalNote = null,
    string? VendorNote = null
) : IRequest<KnResult<PurchaseOrder>>;

public sealed record PurchaseItemDto(
    string Title,
    int Quantity,
    string? Author = null,
    string? Publisher = null,
    string? Edition = null,
    int? PublishingYear = null,
    string? Isbn = null,
    decimal? VendorPrice = null,
    string? InternalNote = null,
    string? VendorNote = null);