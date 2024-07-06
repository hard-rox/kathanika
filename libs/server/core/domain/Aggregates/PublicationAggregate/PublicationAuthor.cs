using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.PublicationAggregate;

public sealed record PublicationAuthor(
    string Id,
    string FirstName,
    string LastName,
    string? DpFileId
) : ValueObject
{
    public string FullName => $"{FirstName} {LastName}";

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new string[] { Id, FirstName, LastName };
    }
}
