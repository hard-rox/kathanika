using System.Linq.Expressions;
using Kathanika.Application.Features.Publications.Commands;

namespace Kathanika.Application.Test.Commands;

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
        List<Author> authors = [
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
            ];
        Publication publication = Publication.Create(
            "Title",
            "ISBN",
            PublicationType.Book,
            "John Doe",
            DateOnly.Parse("2023-01-01"),
            "",
            (decimal)100.50,
            2,
            "ABCD123",
            string.Empty,
            string.Empty,
            authors
        );

        AddPublicationCommandHandler handler = new(publicationRepository, authorRepository);
        authorRepository.ListAllAsync(Arg.Any<Expression<Func<Author, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(authors);
        publicationRepository.AddAsync(Arg.Any<Publication>(), Arg.Any<CancellationToken>())
            .Returns(Publication.Create(
            publication.Title,
            publication.Isbn,
            publication.PublicationType,
            publication.Publisher,
            publication.PublishedDate,
            publication.Edition,
            publication.BuyingPrice,
            publication.CopiesAvailable,
            publication.CallNumber,
            publication.Description,
            publication.Language,
            authors
        ));

        AddPublicationCommand command = new(
            publication.Title,
            publication.Isbn ?? "",
            publication.PublicationType,
            new List<string>(),
            publication.Publisher,
            publication.PublishedDate,
            publication.Edition,
            publication.BuyingPrice,
            publication.CopiesAvailable,
            publication.CallNumber
        );

        // Act
        Publication savedPublication = await handler.Handle(command, default);

        // Assert
        Assert.NotNull(savedPublication);
        Assert.Equal(publication.Title, savedPublication.Title);
        await publicationRepository.Received(1).AddAsync(Arg.Is<Publication>(x => x.Title == publication.Title), Arg.Any<CancellationToken>());
        await authorRepository.Received(1).ListAllAsync(Arg.Any<Expression<Func<Author, bool>>>(), Arg.Any<CancellationToken>());
    }
}
