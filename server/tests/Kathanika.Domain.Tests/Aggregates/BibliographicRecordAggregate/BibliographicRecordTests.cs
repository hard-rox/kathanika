using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Domain.Tests.Aggregates.BibliographicRecordAggregate;

public class BibRecordTests
{
    [Fact]
    public void Create_ShouldReturnSuccess_WithValidInputs()
    {
        // Arrange
        Faker faker = new();
        string leader = faker.Random.String2(24);
        string controlNumber = faker.Random.String2(10, 49);
        string controlNumberIdentifier = faker.Random.String2(10, 49);
        DateTime dateTimeOfLatestTransaction = faker.Date.Recent();
        string fixedLengthDataElements = faker.Random.String2(40);
        CatalogingSource catalogingSource = CatalogingSource.Create(null, null, faker.Company.CompanyName(), null, null).Value;

        // Act
        Result<BibRecord> result = BibRecord.Create(
            leader,
            controlNumber,
            controlNumberIdentifier,
            dateTimeOfLatestTransaction,
            fixedLengthDataElements,
            catalogingSource);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WithEmptyLeader()
    {
        // Arrange
        Faker faker = new();
        string emptyLeader = string.Empty;
        string controlNumber = faker.Random.String2(10, 49);
        string controlNumberIdentifier = faker.Random.String2(10, 49);
        DateTime dateTimeOfLatestTransaction = faker.Date.Recent();
        string fixedLengthDataElements = faker.Random.String2(40);
        CatalogingSource catalogingSource = CatalogingSource.Create(null, null, faker.Company.CompanyName(), null, null).Value;

        // Act
        Result<BibRecord> result = BibRecord.Create(
            emptyLeader,
            controlNumber,
            controlNumberIdentifier,
            dateTimeOfLatestTransaction,
            fixedLengthDataElements,
            catalogingSource);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == BibRecordAggregateErrors.LeaderInvalid.Code);
    }
}
