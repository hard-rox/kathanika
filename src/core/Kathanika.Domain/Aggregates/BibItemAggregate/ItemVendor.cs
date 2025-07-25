using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibItemAggregate;

public sealed record ItemVendor(string Id, string Name) : ValueObject
{
    private ItemVendor() : this(string.Empty, string.Empty)
    {
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Id;
        yield return Name;
    }
}