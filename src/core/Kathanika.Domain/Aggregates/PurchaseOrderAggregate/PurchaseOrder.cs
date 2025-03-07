using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

public sealed class PurchaseOrder : AggregateRoot
{
    private readonly List<PurchaseItem> _items = [];

    private PurchaseOrder()
    {

    }

    public string VendorId { get; private set; }
    public string VendorName { get; private set; }
    public string? InternalNote { get; private set; }
    public string? VendorNote { get; private set; }

    public IReadOnlyCollection<PurchaseItem> PurchaseItems
    {
        get => _items;
        init => _items = value?.ToList() ?? [];
    }

    private PurchaseOrder(
        string vendorId,
        string vendorName,
        IEnumerable<PurchaseItem> purchaseItems,
        string? internalNote = null,
        string? vendorNote = null)
    {
        VendorId = vendorId;
        VendorName = vendorName;
        _items = purchaseItems?.ToList() ?? [];
        InternalNote = internalNote;
        VendorNote = vendorNote;
    }

    public static PurchaseOrder Create(
        string vendorId,
        string vendorName,
        IEnumerable<PurchaseItem> purchaseItems,
        string? internalNote = null,
        string? vendorNote = null)
    {
        return new PurchaseOrder(
            vendorId,
            vendorName,
            purchaseItems,
            internalNote,
            vendorNote);
    }
}