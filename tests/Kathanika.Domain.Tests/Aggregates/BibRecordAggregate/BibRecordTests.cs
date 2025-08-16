using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Domain.Tests.Aggregates.BibRecordAggregate;

/// <summary>
/// Unit tests for BibRecord aggregate root.
/// Tests the public interface and behavior of BibRecord.
/// Note: Some integration tests are skipped due to MarcMetadata implementation issues.
/// </summary>
public sealed class BibRecordTests
{
    #region Property Tests - Null MarcMetadata Scenarios

    [Fact]
    public void Title_ShouldReturnEmptyString_WhenMarcMetadataIsNull()
    {
        // Arrange - Use reflection to create BibRecord with null MarcMetadata
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        var title = bibRecord.Title;

        // Assert
        Assert.Equal(string.Empty, title);
    }

    [Fact]
    public void Author_ShouldReturnEmptyString_WhenMarcMetadataIsNull()
    {
        // Arrange - Use reflection to create BibRecord with null MarcMetadata
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        var author = bibRecord.Author;

        // Assert
        Assert.Equal(string.Empty, author);
    }

    [Fact]
    public void ControlNumber_ShouldReturnEmptyString_WhenMarcMetadataIsNull()
    {
        // Arrange - Use reflection to create BibRecord with null MarcMetadata
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        var controlNumber = bibRecord.ControlNumber;

        // Assert
        Assert.Equal(string.Empty, controlNumber);
    }

    [Fact]
    public void Isbn_ShouldReturnEmptyString_WhenMarcMetadataIsNull()
    {
        // Arrange - Use reflection to create BibRecord with null MarcMetadata
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        var isbn = bibRecord.Isbn;

        // Assert
        Assert.Equal(string.Empty, isbn);
    }

    [Fact]
    public void Issn_ShouldReturnEmptyString_WhenMarcMetadataIsNull()
    {
        // Arrange - Use reflection to create BibRecord with null MarcMetadata
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        var issn = bibRecord.Issn;

        // Assert
        Assert.Equal(string.Empty, issn);
    }

    [Fact]
    public void Publisher_ShouldReturnEmptyString_WhenMarcMetadataIsNull()
    {
        // Arrange - Use reflection to create BibRecord with null MarcMetadata
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        var publisher = bibRecord.Publisher;

        // Assert
        Assert.Equal(string.Empty, publisher);
    }

    [Fact]
    public void PublicationYear_ShouldReturnNull_WhenMarcMetadataIsNull()
    {
        // Arrange - Use reflection to create BibRecord with null MarcMetadata
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        var publicationYear = bibRecord.PublicationYear;

        // Assert
        Assert.Null(publicationYear);
    }

    [Fact]
    public void MaterialType_ShouldReturnNull_WhenMarcMetadataIsNull()
    {
        // Arrange - Use reflection to create BibRecord with null MarcMetadata
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        var materialType = bibRecord.MaterialType;

        // Assert
        Assert.Null(materialType);
    }

    [Fact]
    public void Note_ShouldReturnNull_WhenMarcMetadataIsNull()
    {
        // Arrange - Use reflection to create BibRecord with null MarcMetadata
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        var note = bibRecord.Note;

        // Assert
        Assert.Null(note);
    }

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

    #endregion

    #region AddCoverImage Tests

    [Fact]
    public void AddCoverImage_ShouldReturnFailure_WhenCoverImageIdIsEmpty()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        KnResult result = bibRecord.AddCoverImage(string.Empty);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "BibRecord.InvalidCoverImageId");
    }

    [Fact]
    public void AddCoverImage_ShouldReturnFailure_WhenCoverImageIdIsWhitespace()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        KnResult result = bibRecord.AddCoverImage("   ");

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "BibRecord.InvalidCoverImageId");
    }

    [Fact]
    public void AddCoverImage_ShouldReturnSuccess_WhenValidCoverImageIdProvided()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;
        var coverImageId = Guid.NewGuid().ToString();

        // Act
        KnResult result = bibRecord.AddCoverImage(coverImageId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(coverImageId, bibRecord.CoverImageId);
    }

    [Fact]
    public void AddCoverImage_ShouldUpdateCoverImageId_WhenCalledMultipleTimes()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;
        var firstCoverImageId = Guid.NewGuid().ToString();
        var secondCoverImageId = Guid.NewGuid().ToString();

        // Act
        KnResult firstResult = bibRecord.AddCoverImage(firstCoverImageId);
        KnResult secondResult = bibRecord.AddCoverImage(secondCoverImageId);

        // Assert
        Assert.True(firstResult.IsSuccess);
        Assert.True(secondResult.IsSuccess);
        Assert.Equal(secondCoverImageId, bibRecord.CoverImageId);
    }

    #endregion

    #region Input Validation Tests

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("\t")]
    [InlineData("\n")]
    public void AddCoverImage_ShouldReturnFailure_WhenCoverImageIdIsWhitespaceVariants(string coverImageId)
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act
        KnResult result = bibRecord.AddCoverImage(coverImageId);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == "BibRecord.InvalidCoverImageId");
        Assert.Contains(result.Errors, e => e.Message == "Cover image ID cannot be null or empty.");
    }

    [Fact]
    public void AddCoverImage_ShouldAcceptValidGuids()
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
            KnResult result = bibRecord.AddCoverImage(coverImageId);

            // Assert
            Assert.True(result.IsSuccess, $"Failed for cover image ID: {coverImageId}");
            Assert.Equal(coverImageId, bibRecord.CoverImageId);
        }
    }

    [Fact]
    public void AddCoverImage_ShouldAcceptNonGuidStrings()
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
            KnResult result = bibRecord.AddCoverImage(coverImageId);

            // Assert
            Assert.True(result.IsSuccess, $"Failed for cover image ID: {coverImageId}");
            Assert.Equal(coverImageId, bibRecord.CoverImageId);
        }
    }

    #endregion

    #region Edge Cases and Boundary Tests

    [Fact]
    public void AddCoverImage_ShouldHandleVeryLongStrings()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;
        var longCoverImageId = new string('A', 1000);

        // Act
        KnResult result = bibRecord.AddCoverImage(longCoverImageId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(longCoverImageId, bibRecord.CoverImageId);
    }

    [Fact]
    public void AddCoverImage_ShouldHandleSpecialCharacters()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;
        var specialCharCoverImageId = "image-with-special-chars-!@#$%^&*()_+-=[]{}|;:,.<>?";

        // Act
        KnResult result = bibRecord.AddCoverImage(specialCharCoverImageId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(specialCharCoverImageId, bibRecord.CoverImageId);
    }

    [Fact]
    public void AddCoverImage_ShouldHandleUnicodeCharacters()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;
        var unicodeCoverImageId = "图像标识符-αβγδε-🖼️📚";

        // Act
        KnResult result = bibRecord.AddCoverImage(unicodeCoverImageId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(unicodeCoverImageId, bibRecord.CoverImageId);
    }

    #endregion

    #region Aggregate State Tests

    [Fact]
    public void BibRecord_ShouldMaintainState_WhenMultipleOperationsPerformed()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;
        var coverImageId1 = "first-image";
        var coverImageId2 = "second-image";
        var coverImageId3 = "third-image";

        // Act
        KnResult result1 = bibRecord.AddCoverImage(coverImageId1);
        KnResult result2 = bibRecord.AddCoverImage(coverImageId2);
        KnResult result3 = bibRecord.AddCoverImage(coverImageId3);

        // Assert
        Assert.True(result1.IsSuccess);
        Assert.True(result2.IsSuccess);
        Assert.True(result3.IsSuccess);
        Assert.Equal(coverImageId3, bibRecord.CoverImageId); // Should have the last one
    }

    [Fact]
    public void BibRecord_ShouldStartWithNullCoverImageId()
    {
        // Arrange & Act
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Assert
        Assert.Null(bibRecord.CoverImageId);
    }

    #endregion

    #region Documentation and Examples

    /// <summary>
    /// This test serves as documentation for the expected behavior of the BibRecord aggregate.
    /// It shows how the aggregate maintains its state and validates input.
    /// </summary>
    [Fact]
    public void BibRecord_ExampleUsage_ShouldDemonstrateCorrectBehavior()
    {
        // Arrange
        BibRecord bibRecord = (BibRecord)Activator.CreateInstance(typeof(BibRecord), true)!;

        // Act & Assert - Initial state
        Assert.Null(bibRecord.CoverImageId);
        Assert.Equal(string.Empty, bibRecord.Title);
        Assert.Equal(string.Empty, bibRecord.Author);
        Assert.Equal(string.Empty, bibRecord.Isbn);
        Assert.Null(bibRecord.PublicationYear);

        // Act & Assert - Adding cover image
        const string coverImageId = "example-cover-123";
        KnResult result = bibRecord.AddCoverImage(coverImageId);

        Assert.True(result.IsSuccess);
        Assert.Equal(coverImageId, bibRecord.CoverImageId);

        // Act & Assert - Invalid input handling
        KnResult invalidResult = bibRecord.AddCoverImage("");
        Assert.True(invalidResult.IsFailure);
        Assert.Contains(invalidResult.Errors, e => e.Code == "BibRecord.InvalidCoverImageId");

        // The Cover image should remain unchanged after the failed operation
        Assert.Equal(coverImageId, bibRecord.CoverImageId);
    }

    #endregion
}