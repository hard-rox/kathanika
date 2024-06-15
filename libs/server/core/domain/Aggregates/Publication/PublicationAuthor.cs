using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates;

public sealed record PublicationAuthor(
    string Id,
    string FirstName,
    string LastName
) : ValueObject
{
    public string FullName => $"{FirstName} {LastName}";

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new string[] { Id, FirstName, LastName };
    }
}
