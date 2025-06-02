using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Domain.Tests.Aggregates.BibliographicRecordAggregate;

public class BibRecordTests
{
    [Fact]
    public void CreateBookRecord_ShouldReturnSuccess_WithValidInputs()
    {
        // Arrange
        Faker faker = new();
        var title = faker.Lorem.Sentence();
        var isbn = $"978-{faker.Random.Number(1000000000, 999999999)}";
        var authorName = faker.Name.FullName();
        var publisherName = faker.Company.CompanyName();
        var publicationDate = faker.Date.Past().Year.ToString();
        var extent = $"{faker.Random.Number(100, 1000)} pages";
        var dimensions = $"{faker.Random.Number(20, 30)} cm";

        // Act
        KnResult<BibRecord> result = BibRecord.CreateBookRecord(
            title,
            isbn,
            authorName,
            publisherName,
            publicationDate,
            extent,
            dimensions);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(title, result.Value.TitleStatement.Title);
        Assert.Equal(isbn, result.Value.InternationalStandardBookNumbers[0]);
        Assert.Equal(authorName, result.Value.MainEntryPersonalName?.PersonalName);
        Assert.Equal(publisherName, result.Value.PublicationDistributions[0].NamesOfPublisher[0]);
        Assert.Equal(publicationDate, result.Value.PublicationDistributions[0].DatesOfPublication[0]);
        Assert.Equal(extent, result.Value.PhysicalDescriptions[0].Extents[0]);
        Assert.Equal(dimensions, result.Value.PhysicalDescriptions[0].Dimensions[0]);
    }

    [Fact]
    public void CreateBookRecord_ShouldReturnFailure_WithEmptyTitle()
    {
        // Arrange
        Faker faker = new();
        var emptyTitle = string.Empty;
        var isbn = $"978-{faker.Random.Number(1000000000, 999999999)}";
        var authorName = faker.Name.FullName();
        var publisherName = faker.Company.CompanyName();
        var publicationDate = faker.Date.Past().Year.ToString();
        var extent = $"{faker.Random.Number(100, 1000)} pages";
        var dimensions = $"{faker.Random.Number(20, 30)} cm";

        // Act
        KnResult<BibRecord> result = BibRecord.CreateBookRecord(
            emptyTitle,
            isbn,
            authorName,
            publisherName,
            publicationDate,
            extent,
            dimensions);

        // Assert
        Assert.True(result.IsFailure);
    }
}