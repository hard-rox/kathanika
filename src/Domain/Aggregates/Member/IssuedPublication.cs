using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class IssuedPublication : ValueObject
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required PublicationType PublicationType { get; init; }
    public required string CallNumber { get; init; }

    public IssuedPublication(
    string id,
    string title,
    PublicationType publicationType,
    string callNumber)
    {
        Id = id;
        Title = title;
        PublicationType = publicationType;
        CallNumber = callNumber;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new object[] { Id, Title, PublicationType, CallNumber };
    }
}
