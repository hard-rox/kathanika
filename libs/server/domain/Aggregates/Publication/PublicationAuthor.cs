using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed record PublicationAuthor(
    string Id,
    string FirstName,
    string LastName
) : ValueObject
{
    public string FullName => $"{FirstName} {LastName}";

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new object[] { Id, FirstName, LastName };
    }
}
