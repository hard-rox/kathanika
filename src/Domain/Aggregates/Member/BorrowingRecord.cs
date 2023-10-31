using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.Member;

public sealed class BorrowingRecord : ValueObject
{
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Array.Empty<object>();
    }
}
