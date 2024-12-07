using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

public record CatalogingSource(
    string? OriginalCatalogingAgency,
    string? LanguageOfCataloging,
    string TranscribingAgency,
    string? ModifyingAgency,
    string? DescriptionConventions) : ValueObject
{
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