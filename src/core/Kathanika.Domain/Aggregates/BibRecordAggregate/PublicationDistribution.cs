using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// Represents MARC21 Field 260 - Publication, Distribution, etc. (R)
/// </summary>
public sealed record PublicationDistribution : ValueObject
{
    private List<string> _placesOfPublication = [];
    private List<string> _namesOfPublisher = [];
    private List<string> _datesOfPublication = [];

    /// <summary>
    /// Place of publication, distribution, etc. (Subfield $a) - Repeatable
    /// </summary>
    public IReadOnlyList<string> PlacesOfPublication
    {
        get => _placesOfPublication;
        private set { _placesOfPublication = value.ToList(); }
    }

    /// <summary>
    /// Name of publisher, distributor, etc. (Subfield $b) - Repeatable
    /// </summary>
    public IReadOnlyList<string> NamesOfPublisher
    {
        get => _namesOfPublisher;
        private set { _namesOfPublisher = value.ToList(); }
    }

    /// <summary>
    /// Date of publication, distribution, etc. (Subfield $c) - Repeatable
    /// </summary>
    public IReadOnlyList<string> DatesOfPublication
    {
        get => _datesOfPublication;
        private set { _datesOfPublication = value.ToList(); }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PublicationDistribution"/> record with optional publisher name and publication date.
    /// </summary>
    /// <param name="publisherName">The name of the publisher or distributor, or <c>null</c> if not specified.</param>
    /// <param name="publicationDate">The date of publication or distribution, or <c>null</c> if not specified.</param>
    internal PublicationDistribution(
        string? publisherName,
        string? publicationDate)
    {
        if (!string.IsNullOrWhiteSpace(publisherName))
            _namesOfPublisher = [publisherName];

        if (!string.IsNullOrWhiteSpace(publicationDate))
            _datesOfPublication = [publicationDate];
    }

    /// <summary>
    /// Returns the lists of places of publication, names of publisher, and dates of publication as atomic values for value object equality comparisons.
    /// </summary>
    /// <returns>An enumerable containing the three publication-related lists.</returns>
    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return PlacesOfPublication;
        yield return NamesOfPublisher;
        yield return DatesOfPublication;
    }
}