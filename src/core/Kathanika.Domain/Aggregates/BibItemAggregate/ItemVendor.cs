using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibItemAggregate;

public record ItemVendor(string Id, string Name) : ValueObject
{
    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Id;
        yield return Name;
    }
}