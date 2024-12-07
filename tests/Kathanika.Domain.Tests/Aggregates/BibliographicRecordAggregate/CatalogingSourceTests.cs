using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Domain.Tests.Aggregates.BibliographicRecordAggregate;

public sealed class CatalogingSourceTests
{
    // Create should return success with valid inputs
    [Fact]
    public void Create_ShouldReturnSuccess_WithValidInputs()
    {
        // Arrange
        Faker faker = new();
        var originalCatalogingAgency = faker.Company.CompanyName();
        var languageOfCataloging = faker.Random.String2(3);
        var transcribingAgency = faker.Company.CompanyName();
        var modifyingAgency = faker.Company.CompanyName();
        var descriptionConventions = faker.Lorem.Sentence();

        // Act
        KnResult<CatalogingSource> result = CatalogingSource.Create(
            originalCatalogingAgency,
            languageOfCataloging,
            transcribingAgency,
            modifyingAgency,
            descriptionConventions);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    // Create should return failure with empty transcribingAgency
    [Fact]
    public void Create_ShouldReturnFailure_WithEmptyTranscribingAgency()
    {
        // Arrange
        Faker faker = new();
        var originalCatalogingAgency = faker.Company.CompanyName();
        var languageOfCataloging = faker.Random.String2(3);
        var emptyTranscribingAgency = string.Empty;
        var modifyingAgency = faker.Company.CompanyName();
        var descriptionConventions = faker.Lorem.Sentence();

        // Act
        KnResult<CatalogingSource> result = CatalogingSource.Create(
            originalCatalogingAgency,
            languageOfCataloging,
            emptyTranscribingAgency,
            modifyingAgency,
            descriptionConventions);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == BibRecordAggregateErrors.TranscribingAgencyInvalid.Code);
    }
}