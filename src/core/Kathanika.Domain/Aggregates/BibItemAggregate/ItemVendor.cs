using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibItemAggregate;

public sealed record ItemVendor : ValueObject
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    private ItemVendor()
    {
    }

    internal ItemVendor(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Id;
        yield return Name;
    }
}