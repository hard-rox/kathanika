using Kathanika.Domain.Aggregates;

namespace Kathanika.Domain.Test;

public class PublicationAggregateTests
{
    [Fact]
    public void Create_Should_Return_Publication_On_Valid_Input()
    {
        // Arrange

        // Act
        Publication publication = Publication.Create(
            "Title",
            "12345",
            PublicationType.Book,
            "Hello",
            DateOnly.MinValue,
            "",
            string.Empty,
            "Nt0202",
            "",
            AcquisitionMethod.Donation,
            10,
            20,
            string.Empty,
            string.Empty,
            [
                Author.Create(
                    "John",
                    "Doe",
                    DateOnly.MinValue,
                    null,
                    "",
                    ""
                ),
                Author.Create(
                    "John",
                    "Doe",
                    DateOnly.MinValue,
                    null,
                    "",
                    ""
                )
            ]
        );

        // Assert
        Assert.Equal("Title", publication.Title);
        Assert.Equal(2, publication.Authors.Count);
    }

    [Fact]
    public void Update_Should_Return_Publication_On_Valid_Input()
    {
        // Arrange
        Publication publication = Publication.Create(
            "Title",
            "12345",
            PublicationType.Book,
            "Hello",
            DateOnly.MinValue,
            "",
            string.Empty,
            "Nt0202",
            "",
            AcquisitionMethod.Donation,
            10,
            20,
            string.Empty,
            string.Empty,
            []
        );

        // Act
        publication.Update(
            "Updated Title",
            "Updated Isbn",
            PublicationType.Journal,
            "Updated publisher",
            DateOnly.MinValue,
            null,
            "Updated CallNumber",
            "",
            ""
        );

        // Assert
        Assert.Equal("Updated Title", publication.Title);
    }
}
