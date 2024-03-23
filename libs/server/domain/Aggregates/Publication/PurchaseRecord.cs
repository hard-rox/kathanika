using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class PurchaseRecord : Entity
{
    public DateOnly PurchasedDate { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }
    public decimal UnitPrice { get; private set; }
    public string Vendor { get; private set; }

    internal PurchaseRecord(
        int quantity,
        decimal unitPrice,
        string vendor
    )
    {
        Quantity = quantity;
        TotalPrice = quantity * unitPrice;
        UnitPrice = unitPrice;
        Vendor = vendor;
    }

    internal void Update(
        DateOnly? purchasedDate = null,
        int? quantity = null,
        decimal? unitPrice = null,
        string? vendor = null
    )
    {
        PurchasedDate = purchasedDate is not null ? (DateOnly)purchasedDate : PurchasedDate;
        Quantity = quantity is not null ? (int)quantity : Quantity;
        TotalPrice = quantity is not null && unitPrice is not null ? (decimal)quantity * (decimal)unitPrice
            : quantity is not null && unitPrice is null ? (decimal)quantity * UnitPrice
            : quantity is null && unitPrice is not null ? Quantity * (decimal)unitPrice
            : TotalPrice;
        UnitPrice = unitPrice is not null ? (decimal)unitPrice : UnitPrice;
        Vendor = string.IsNullOrEmpty(vendor) ? Vendor : vendor;
    }
}
