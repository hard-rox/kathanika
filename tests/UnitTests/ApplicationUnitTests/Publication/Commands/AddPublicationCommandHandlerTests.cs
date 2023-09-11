using System.Linq.Expressions;
using Kathanika.Application.Publications.Commands;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class AddPublicationCommandHandlerTests
{
    private readonly IPublicationRepository publicationRepository;
    private readonly IAuthorRepository authorRepository;

    public AddPublicationCommandHandlerTests()
    {
        publicationRepository = Substitute.For<IPublicationRepository>();
        authorRepository = Substitute.For<IAuthorRepository>();
    }


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

        var handler = new AddPublicationCommandHandler(publicationRepository, authorRepository);
        authorRepository.ListAllAsync(Arg.Any<Expression<Func<Author, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(authors);
        publicationRepository.AddAsync(Arg.Any<Publication>(), Arg.Any<CancellationToken>())
            .Returns(Publication.Create(
            publication.Title,
            publication.Isbn,
            publication.PublicationType,
            publication.Publisher,
            publication.PublishedDate,
            publication.BuyingPrice,
            publication.CopiesAvailable,
            publication.CallNumber,
            authors
        ));

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
        await publicationRepository.Received(1).AddAsync(Arg.Is<Publication>(x => x.Title == publication.Title), Arg.Any<CancellationToken>());
        await authorRepository.Received(1).ListAllAsync(Arg.Any<Expression<Func<Author, bool>>>(), Arg.Any<CancellationToken>());
    }
}