namespace Kathanika.UnitTests.DomainUnitTests;

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
            (decimal)102.0,
            1,
            "Nt0202",
            new List<Author>(){
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
            }
        );

        // Assert
        Assert.Equal("Title", publication.Title);
        Assert.Equal(2, publication.Authors.Count());
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
            (decimal)102.0,
            1,
            "ANC0123",
            new List<Author>()
        );

        // Act
        publication.Update(
            "Updated Title",
            "Updated Isbn",
            PublicationType.Journal,
            "Updated publisher",
            DateOnly.MinValue,
            null,
            (decimal)10.2,
            3,
            "Updated CallNumber"
        );

        // Assert
        Assert.Equal("Updated Title", publication.Title);
    }
}
