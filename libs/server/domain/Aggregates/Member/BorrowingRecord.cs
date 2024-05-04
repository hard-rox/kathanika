using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed record BorrowingRecord : ValueObject
{
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Array.Empty<object>();
    }
}
