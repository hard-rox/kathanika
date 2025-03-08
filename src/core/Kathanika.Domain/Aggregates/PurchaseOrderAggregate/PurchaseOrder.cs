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

    public KnResult Update(
        string? vendorId,
        string? vendorName,
        string? internalNote,
        string? vendorNote)
    {
        VendorId = !string.IsNullOrWhiteSpace(vendorId) ? vendorId : VendorId;
        VendorName = !string.IsNullOrWhiteSpace(vendorName) ? vendorName : VendorName;
        InternalNote = !string.IsNullOrWhiteSpace(internalNote) ? internalNote : InternalNote;
        VendorNote = !string.IsNullOrWhiteSpace(vendorNote) ? vendorNote : VendorNote;

        return KnResult.Success();
    }
}