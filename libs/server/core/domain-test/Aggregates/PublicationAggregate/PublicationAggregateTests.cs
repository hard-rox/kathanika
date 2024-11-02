namespace Kathanika.Core.Domain.Test.Aggregates.PublicationAggregate;

public class PublicationAggregateTests
{
    [Fact]
    public void Create_Should_Return_ResultOfPublication_On_Valid_Input()
    {
        // Arrange

        // Act
        Result<Publication> result = Publication.Create(
            "Title",
            "12345",
            PublicationType.Book,
            DateOnly.MinValue,
            string.Empty,
            "Nt0202",
            "",
            "",
            string.Empty,
            AcquisitionMethod.Donation,
            10,
            20,
            string.Empty,
            string.Empty
        );

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Title", result.Value.Title);
    }

    [Fact]
    public void Update_Should_Return_Publication_On_Valid_Input()
    {
        // Arrange
        Publication publication = Publication.Create(
            "Title",
            "12345",
            PublicationType.Book,
            DateOnly.MinValue,
            "",
            string.Empty,
            "Nt0202",
            "",
            "",
            AcquisitionMethod.Donation,
            10,
            20,
            string.Empty,
            string.Empty
        ).Value;

        // Act
        Result result = publication.Update(
            "Updated Title",
            "Updated Isbn",
            PublicationType.Journal,
            DateOnly.MinValue,
            null,
            "Updated CallNumber",
            "",
            "",
            ""
        );

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Updated Title", publication.Title);
    }
}
