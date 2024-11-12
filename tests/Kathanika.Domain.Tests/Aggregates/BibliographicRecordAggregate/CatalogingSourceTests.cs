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
        string originalCatalogingAgency = faker.Company.CompanyName();
        string languageOfCataloging = faker.Random.String2(3);
        string transcribingAgency = faker.Company.CompanyName();
        string modifyingAgency = faker.Company.CompanyName();
        string descriptionConventions = faker.Lorem.Sentence();

        // Act
        Result<CatalogingSource> result = CatalogingSource.Create(
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
        string originalCatalogingAgency = faker.Company.CompanyName();
        string languageOfCataloging = faker.Random.String2(3);
        string emptyTranscribingAgency = string.Empty;
        string modifyingAgency = faker.Company.CompanyName();
        string descriptionConventions = faker.Lorem.Sentence();

        // Act
        Result<CatalogingSource> result = CatalogingSource.Create(
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