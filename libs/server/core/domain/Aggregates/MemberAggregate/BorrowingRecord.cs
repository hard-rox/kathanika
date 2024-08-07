using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.MemberAggregate;

public sealed record BorrowingRecord : ValueObject
{
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Array.Empty<object>();
    }
}
