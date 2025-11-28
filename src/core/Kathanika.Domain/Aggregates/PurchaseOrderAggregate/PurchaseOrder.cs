using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

public sealed class PurchaseOrder : AggregateRoot
{
    private PurchaseOrder()
    {
    }

    public string VendorId { get; private set; }
    public string VendorName { get; private set; }
    public string? InternalNote { get; private set; }
    public string? VendorNote { get; private set; }
    public PurchaseOrderStatus Status { get; private set; }
    public int TotalQuantity => PurchaseItems.Sum(i => i.Quantity);
    public decimal TotalCost => PurchaseItems.Sum(i => (i.VendorPrice ?? 0) * i.Quantity);
    public DateOnly OrderDate => DateOnly.FromDateTime(CreatedAt.Date);

    public IReadOnlyCollection<PurchaseItem> PurchaseItems
    {
        get => field;
        init => field = value?.ToList() ?? [];
    } = new List<PurchaseItem>();

    private PurchaseOrder(
        string vendorId,
        string vendorName,
        IEnumerable<PurchaseItem> purchaseItems,
        string? internalNote = null,
        string? vendorNote = null)
    {
        VendorId = vendorId;
        VendorName = vendorName;
        PurchaseItems = purchaseItems?.ToList() ?? [];
        InternalNote = internalNote;
        VendorNote = vendorNote;
        Status = PurchaseOrderStatus.Pending;
    }

    public static KnResult<PurchaseOrder> Create(
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