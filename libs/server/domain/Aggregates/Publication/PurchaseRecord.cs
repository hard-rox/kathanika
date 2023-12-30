using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed record PurchaseRecord(
    decimal UnitPrice,
    uint Quantity,
    string Vendor
) : ValueObject
{
    public DateOnly PurchasedDate { get; private init; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public decimal TotalPrice { get; private init; } = UnitPrice * Quantity;
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new object[] { UnitPrice, Quantity, Vendor, PurchasedDate, TotalPrice };
    }
}
