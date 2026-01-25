using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;
namespace Kathanika.Application.Features.PurchaseOrders.Commands;

public sealed record UpdatePurchaseOrderCommand(string Id, PurchaseOrderPatch Patch) : ICommand<KnResult<PurchaseOrder>>;

public sealed record PurchaseOrderPatch(
    string? VendorId,
    string? InternalNote,
    string? VendorNote);