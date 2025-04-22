using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// Represents MARC21 040 Cataloging Source field
/// </summary>
public record CatalogingSource : ValueObject
{
    /// <summary>
    /// Original cataloging agency (NR)
    /// MARC organization code of the organization that created the original record.
    /// </summary>
    public string? OriginalCatalogingAgency { get; }

    /// <summary>
    /// Language of cataloging (NR)
    /// MARC code that indicates the language of cataloging in the record.
    /// </summary>
    public string? LanguageOfCataloging { get; }

    /// <summary>
    /// Transcribing agency (NR)
    /// MARC organization code of the organization that transcribed the record into machine-readable form.
    /// </summary>
    public string TranscribingAgency { get; }

    /// <summary>
    /// Modifying agency (R)
    /// MARC organization code of the organization that modified the record.
    /// </summary>
    public string? ModifyingAgency { get; }

    /// <summary>
    /// Description conventions (R)
    /// Information about the rules used for the descriptive cataloging.
    /// </summary>
    public string? DescriptionConventions { get; }

    private CatalogingSource(
        string? originalCatalogingAgency,
        string? languageOfCataloging,
        string transcribingAgency,
        string? modifyingAgency,
        string? descriptionConventions)
    {
        OriginalCatalogingAgency = originalCatalogingAgency;
        LanguageOfCataloging = languageOfCataloging;
        TranscribingAgency = transcribingAgency;
        ModifyingAgency = modifyingAgency;
        DescriptionConventions = descriptionConventions;
    }

    internal static KnResult<CatalogingSource> Create(string? originalCatalogingAgency,
        string? languageOfCataloging,
        string transcribingAgency,
        string? modifyingAgency,
        string? descriptionConventions)
    {
        return string.IsNullOrWhiteSpace(transcribingAgency)
            ? KnResult.Failure<CatalogingSource>(BibRecordAggregateErrors.TranscribingAgencyInvalid)
            : KnResult.Success(new CatalogingSource(originalCatalogingAgency, languageOfCataloging, transcribingAgency,
                modifyingAgency, descriptionConventions));
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return OriginalCatalogingAgency;
        yield return LanguageOfCataloging;
        yield return TranscribingAgency;
        yield return ModifyingAgency;
        yield return DescriptionConventions;
    }
}