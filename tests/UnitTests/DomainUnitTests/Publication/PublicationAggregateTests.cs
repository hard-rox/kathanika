using Kathanika.Domain.Aggregates;

namespace Kathanika.UnitTests.DomainUnitTests;

public class PublicationAggregateTests
{
    [Fact]
    public void Create_Should_Return_Publication_On_Valid_Input()
    {
        // Arrange

        // Act
        var publication = Publication.Create(
            "Title",
            "12345",
            PublicationType.Book,
            "Hello",
            DateTime.MinValue,
            (decimal)102.0,
            1,
            "BACD",
            new List<Author>(){
                Author.Create(
                    "John",
                    "Doe",
                    DateTime.MinValue,
                    null,
                    "",
                    ""
                ),
                Author.Create(
                    "John",
                    "Doe",
                    DateTime.MinValue,
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
        var publication = Publication.Create(
            "Title",
            "12345",
            PublicationType.Book,
            "Hello",
            DateTime.MinValue,
            (decimal)102.0,
            1,
            "BACD",
            new List<Author>()
        );

        // Act
        publication.Update(
            "Updated Title",
            "Updated Isbn",
            PublicationType.Journal,
            "Updated publisher",
            DateTime.MinValue,
            (decimal)10.2,
            3,
            "Updated CallNumber",
            null
        );

        // Assert
        Assert.Equal("Updated Title", publication.Title);
    }
}