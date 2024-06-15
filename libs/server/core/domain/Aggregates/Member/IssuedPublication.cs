using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates;

public sealed record IssuedPublication(
string Id,
string Title,
PublicationType PublicationType,
string CallNumber) : ValueObject
{
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new object[] { Id, Title, PublicationType, CallNumber };
    }
}
