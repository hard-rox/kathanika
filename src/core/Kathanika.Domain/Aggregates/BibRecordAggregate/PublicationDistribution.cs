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

    private PublicationDistribution() { }

    internal PublicationDistribution(
        string? publisherName,
        string? publicationDate)
    {
        if (!string.IsNullOrWhiteSpace(publisherName))
            _namesOfPublisher = [publisherName];

        if (!string.IsNullOrWhiteSpace(publicationDate))
            _datesOfPublication = [publicationDate];
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return PlacesOfPublication;
        yield return NamesOfPublisher;
        yield return DatesOfPublication;
    }
}