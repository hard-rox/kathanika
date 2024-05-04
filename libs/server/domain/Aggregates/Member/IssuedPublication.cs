using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

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
