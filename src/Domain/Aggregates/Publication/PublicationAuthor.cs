using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class PublicationAuthor : ValueObject
{
    public string Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }

    internal PublicationAuthor(
        string id,
        string firstName,
        string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new object[] { Id, FirstName, LastName };
    }
}
