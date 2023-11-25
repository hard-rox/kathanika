using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class IssuedPublication(
string id,
string title,
PublicationType publicationType,
string callNumber) : ValueObject
{
    public required string Id { get; init; } = id;
    public required string Title { get; init; } = title;
    public required PublicationType PublicationType { get; init; } = publicationType;
    public required string CallNumber { get; init; } = callNumber;

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return new object[] { Id, Title, PublicationType, CallNumber };
    }
}
