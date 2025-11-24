using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Domain.Tests.Aggregates.BibRecordAggregate;

public sealed class BibRecordTests
{
    [Fact]
    public void CoverImageId_ShouldReturnNull_WhenNotSet()
    {
        // Arrange - Use reflection to create BibRecord with null MarcMetadata
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        var coverImageId = bibRecord.CoverImageId;

        // Assert
        Assert.Null(coverImageId);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void UpdateCoverImage_ShouldReturnFailure_WhenCoverImageIdIsWhitespaceVariants(string coverImageId)
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        KnResult result = bibRecord.UpdateCoverImage(coverImageId);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "BibRecord.InvalidCoverImageId");
        Assert.Contains(result.Errors, e => e.Message == "Cover image ID cannot be null or empty.");
    }

    [Fact]
    public void UpdateCoverImage_ShouldAcceptValidGuids()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;
        var testCases = new[]
        {
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString("N"),
            Guid.NewGuid().ToString("D"),
            Guid.NewGuid().ToString("B"),
            Guid.NewGuid().ToString("P")
        };

        foreach (var coverImageId in testCases)
        {
            // Act
            KnResult result = bibRecord.UpdateCoverImage(coverImageId);

            // Assert
            Assert.True(result.IsSuccess, $"Failed for cover image ID: {coverImageId}");
            Assert.Equal(coverImageId, bibRecord.CoverImageId);
        }
    }

    [Fact]
    public void UpdateCoverImage_ShouldAcceptNonGuidStrings()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;
        var testCases = new[]
        {
            "image123",
            "cover-image-001",
            "FILE_12345",
            "https://example.com/image.jpg",
            "123456789"
        };

        foreach (var coverImageId in testCases)
        {
            // Act
            KnResult result = bibRecord.UpdateCoverImage(coverImageId);

            // Assert
            Assert.True(result.IsSuccess, $"Failed for cover image ID: {coverImageId}");
            Assert.Equal(coverImageId, bibRecord.CoverImageId);
        }
    }

    [Fact]
    public void CreateBookRecord_ShouldReturnSuccess_WhenValidParametersProvided()
    {
        // Arrange
        const string title = "Test Book Title";
        const string author = "Test Author";
        const string isbn = "978-0123456789";
        const string publisher = "Test Publisher";
        const int publicationYear = 2023;
        const string language = "eng";
        const long numberOfPages = 250L;

        // Act
        KnResult<BibRecord> result =
            BibRecord.CreateBookRecord(title, author, isbn, publisher, publicationYear, language, numberOfPages);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.NotNull(result.Value.MarcMetadata);
    }

    [Fact]
    public void CreateBookRecord_ShouldSetCorrectMetadata_WhenValidParametersProvided()
    {
        // Arrange
        var title = "Sample Book";
        var author = "John Doe";
        var isbn = "978-1234567890";
        var publisher = "Sample Publisher";
        var publicationYear = 2023;
        var language = "eng";
        var numberOfPages = 300L;

        // Act
        KnResult<BibRecord> result =
            BibRecord.CreateBookRecord(title, author, isbn, publisher, publicationYear, language, numberOfPages);

        // Assert
        BibRecord bibRecord = result.Value;
        Assert.Equal(title, bibRecord.Title);
        Assert.Equal(author, bibRecord.Author);
        Assert.Equal(isbn, bibRecord.Isbns);
        Assert.Equal(publisher, bibRecord.Publisher);
        Assert.Equal(publicationYear, bibRecord.PublicationYear);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData(null)]
    public void UpdateEdition_ShouldReturnFailure_WhenEditionIsNullOrWhitespace(string? edition)
    {
        // Arrange
        KnResult<BibRecord> result =
            BibRecord.CreateBookRecord("Title", "Author", "ISBN", "Publisher", 2023, "eng", 100);
        BibRecord bibRecord = result.Value;

        // Act
        KnResult addResult = bibRecord.UpdateEdition(edition);

        // Assert
        Assert.True(addResult.IsFailure);
        Assert.Contains(addResult.Errors, e => e.Code == "BibRecord.InvalidEdition");
        Assert.Contains(addResult.Errors, e => e.Message == "Edition cannot be null or empty.");
    }

    [Fact]
    public void UpdateEdition_ShouldReturnSuccess_WhenValidEditionProvided()
    {
        // Arrange
        KnResult<BibRecord> result =
            BibRecord.CreateBookRecord("Title", "Author", "ISBN", "Publisher", 2023, "eng", 100);
        BibRecord bibRecord = result.Value;
        var edition = "2nd Edition";

        // Act
        KnResult addResult = bibRecord.UpdateEdition(edition);

        // Assert
        Assert.True(addResult.IsSuccess);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    [InlineData(null)]
    public void UpdateNote_ShouldReturnFailure_WhenNoteIsNullOrWhitespace(string? note)
    {
        // Arrange
        KnResult<BibRecord> result =
            BibRecord.CreateBookRecord("Title", "Author", "ISBN", "Publisher", 2023, "eng", 100);
        BibRecord bibRecord = result.Value;

        // Act
        KnResult addResult = bibRecord.UpdateNote(note);

        // Assert
        Assert.True(addResult.IsFailure);
        Assert.Contains(addResult.Errors, e => e.Code == "BibRecord.InvalidNote");
        Assert.Contains(addResult.Errors, e => e.Message == "Note cannot be null or empty.");
    }

    [Fact]
    public void UpdateNote_ShouldReturnSuccess_WhenValidNoteProvided()
    {
        // Arrange
        KnResult<BibRecord> result =
            BibRecord.CreateBookRecord("Title", "Author", "ISBN", "Publisher", 2023, "eng", 100);
        BibRecord bibRecord = result.Value;
        const string note = "This is a test note";

        // Act
        KnResult addResult = bibRecord.UpdateNote(note);

        // Assert
        Assert.True(addResult.IsSuccess);
    }

    [Theory]
    [InlineData(2023)]
    [InlineData(1995)]
    [InlineData(2000)]
    public void PublicationYear_ShouldParseCorrectly_WhenValidYearInMarc(int expectedYear)
    {
        // Arrange
        KnResult<BibRecord> result =
            BibRecord.CreateBookRecord("Title", "Author", "ISBN", "Publisher", expectedYear, "eng", 100);
        BibRecord bibRecord = result.Value;

        // Act
        var actualYear = bibRecord.PublicationYear;

        // Assert
        Assert.Equal(expectedYear, actualYear);
    }

    [Fact]
    public void Properties_ShouldReturnExpectedValues_WhenMarcMetadataContainsData()
    {
        // Arrange
        const string title = "Advanced Programming";
        const string author = "Jane Smith";
        const string isbn = "978-9876543210";
        const string publisher = "Tech Books";
        const int year = 2024;

        // Act
        KnResult<BibRecord> result = BibRecord.CreateBookRecord(title, author, isbn, publisher, year, "eng", 500);
        BibRecord bibRecord = result.Value;

        // Assert
        Assert.Equal(title, bibRecord.Title);
        Assert.Equal(author, bibRecord.Author);
        Assert.Equal(isbn, bibRecord.Isbns);
        Assert.Equal(publisher, bibRecord.Publisher);
        Assert.Equal(year, bibRecord.PublicationYear);
        Assert.NotNull(bibRecord.MarcMetadata);
    }

    [Fact]
    public void UpdateCoverImage_ShouldUpdateCoverImageId_WhenValidIdProvided()
    {
        // Arrange
        KnResult<BibRecord> result =
            BibRecord.CreateBookRecord("Title", "Author", "ISBN", "Publisher", 2023, "eng", 100);
        BibRecord bibRecord = result.Value;
        const string coverImageId = "cover-123";

        // Act
        KnResult addResult = bibRecord.UpdateCoverImage(coverImageId);

        // Assert
        Assert.True(addResult.IsSuccess);
        Assert.Equal(coverImageId, bibRecord.CoverImageId);
    }

    [Fact]
    public void UpdateCoverImage_ShouldOverwritePreviousValue_WhenCalledMultipleTimes()
    {
        // Arrange
        KnResult<BibRecord> result =
            BibRecord.CreateBookRecord("Title", "Author", "ISBN", "Publisher", 2023, "eng", 100);
        BibRecord bibRecord = result.Value;
        const string firstId = "cover-001";
        const string secondId = "cover-002";

        // Act
        bibRecord.UpdateCoverImage(firstId);
        KnResult finalResult = bibRecord.UpdateCoverImage(secondId);

        // Assert
        Assert.True(finalResult.IsSuccess);
        Assert.Equal(secondId, bibRecord.CoverImageId);
    }
}