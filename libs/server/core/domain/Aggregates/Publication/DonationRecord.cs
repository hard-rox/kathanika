using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates;

public sealed class DonationRecord(int quantity, string patron) : Entity
{
    public DateOnly DonationDate { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public int Quantity { get; private set; } = quantity;
    public string Patron { get; private set; } = patron;
}
