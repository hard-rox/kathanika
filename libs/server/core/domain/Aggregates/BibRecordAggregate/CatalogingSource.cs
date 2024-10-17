using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.BibRecordAggregate;

public record CatalogingSource(string? OriginalCatalogingAgency,
    string? LanguageOfCataloging,
    string TranscribingAgency,
    string? ModifyingAgency,
    string? DescriptionConventions) : ValueObject
{
    internal static Result<CatalogingSource> Create(string? originalCatalogingAgency,
        string? languageOfCataloging,
        string transcribingAgency,
        string? modifyingAgency,
        string? descriptionConventions)
    {
        if (string.IsNullOrWhiteSpace(transcribingAgency))
        {
            return Result.Failure<CatalogingSource>(BibRecordAggregateErrors.TranscribingAgencyInvalid);
        }

        return Result.Success(new CatalogingSource(originalCatalogingAgency, languageOfCataloging, transcribingAgency, modifyingAgency, descriptionConventions));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return OriginalCatalogingAgency;
        yield return LanguageOfCataloging;
        yield return TranscribingAgency;
        yield return ModifyingAgency;
        yield return DescriptionConventions;
    }
}
