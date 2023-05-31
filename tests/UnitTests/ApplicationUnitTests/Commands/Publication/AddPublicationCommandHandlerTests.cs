using System.Linq.Expressions;
using Kathanika.Application.Commands;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class AddPublicationCommandHandlerTests
{
    private readonly Mock<IPublicationRepository> publicationRepositoryMock = new();
    private readonly Mock<IAuthorRepository> authorRepositoryMock = new();

    [Fact]
    public async Task Handler_Should_Return_Saved_Publication_On_Valid_Input()
    {
        // Arrange
        var authors = new List<Author>(){
                Author.Create(
                    "John",
                    "Doe",
                    DateOnly.MinValue,
                    null,
                    "USA",
                    "A good writer"
                ),
                Author.Create(
                    "Jane",
                    "Doe",
                    DateOnly.MinValue,
                    null,
                    "USA",
                    "Another good writer"
                )
            };
        var publication = Publication.Create(
            "Title",
            "ISBN",
            PublicationType.Book,
            "John Doe",
            DateOnly.Parse("2023-01-01"),
            (decimal)100.50,
            2,
            "ABCD123",
            authors
        );

        var handler = new AddPublicationCommandHandler(publicationRepositoryMock.Object, authorRepositoryMock.Object);
        authorRepositoryMock.Setup(x => x.ListAllAsync(It.IsAny<Expression<Func<Author, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(authors).Verifiable();
        publicationRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Publication>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Publication.Create(
            publication.Title,
            publication.Isbn,
            publication.PublicationType,
            publication.Publisher,
            publication.PublishedDate,
            publication.BuyingPrice,
            publication.CopiesAvailable,
            publication.CallNumber,
            authors
        )).Verifiable();

        var command = new AddPublicationCommand(
            publication.Title,
            publication.Isbn ?? "",
            publication.PublicationType,
            new List<string>(),
            publication.Publisher,
            publication.PublishedDate,
            publication.BuyingPrice,
            publication.CopiesAvailable,
            publication.CallNumber
        );

        // Act
        var savedPublication = await handler.Handle(command, default);

        // Assert
        Assert.NotNull(savedPublication);
        Assert.Equal(publication.Title, savedPublication.Title);
        publicationRepositoryMock.Verify(x => x.AddAsync(It.Is<Publication>(x => x.Title == publication.Title), It.IsAny<CancellationToken>()), Times.Exactly(1));
        authorRepositoryMock.Verify(x => x.ListAllAsync(It.IsAny<Expression<Func<Author, bool>>>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
    }
}