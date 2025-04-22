using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;


public record PublicationDistribution : ValueObject
{
    /// <summary>
    /// 260 (Publication, Distribution, etc. (Imprint)) - Optional
    /// Contains information about the publication, distribution, and manufacture of the resource.
    /// </summary>
    public string? PublisherName { get; }

    /// <summary>
    /// 260 (Publication, Distribution, etc. (Imprint)) - Optional
    /// Contains information about the publication, distribution, and manufacture of the resource.
    /// </summary>
    public string? PlaceOfPublication { get; }

    /// <summary>
    /// 260 (Publication, Distribution, etc. (Imprint)) - Optional
    /// Contains information about the publication, distribution, and manufacture of the resource.
    /// </summary>
    public string? DateOfPublication { get; }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return PublisherName;
        yield return PlaceOfPublication;
        yield return DateOfPublication;
    }
}