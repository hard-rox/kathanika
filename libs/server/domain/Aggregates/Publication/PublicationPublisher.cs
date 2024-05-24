using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;
public sealed record PublicationPublisher(
    string Id,
    string Name
) : ValueObject
{
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new string[] { Id, Name };
    }
}

