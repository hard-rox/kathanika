using System.Linq.Expressions;
using Kathanika.Application.Features.Publications.Commands;

namespace Kathanika.Application.Test.Commands;

public class AcquirePublicationCommandHandlerTests
{
    private readonly IPublicationRepository publicationRepository;
    private readonly IAuthorRepository authorRepository;
    private readonly IPublisherRepository publisherRepository;

    public AcquirePublicationCommandHandlerTests()
    {
        publicationRepository = Substitute.For<IPublicationRepository>();
        authorRepository = Substitute.For<IAuthorRepository>();
        publisherRepository = Substitute.For<IPublisherRepository>();
    }

    [Fact]
    public async Task Handler_Should_Return_Saved_Publication_On_Valid_Input()
    {
        // Arrange
        Publisher publisher = Publisher.Create("John wick");
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
            DateOnly.Parse("2023-01-01"),
            "",
            "ABCD123",
            string.Empty,
            string.Empty,
            AcquisitionMethod.Purchase,
            11,
            publisher,
            12,
            string.Empty,
            string.Empty,
            authors
        );

        AcquirePublicationCommandHandler handler = new(publicationRepository, authorRepository, publisherRepository);
        authorRepository.ListAllAsync(Arg.Any<Expression<Func<Author, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(authors);
        publicationRepository.AddAsync(Arg.Any<Publication>(), Arg.Any<CancellationToken>())
            .Returns(publication);
        publisherRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(publisher);

        AcquirePublicationCommand command = new(
            "Title",
            "ISBN",
            PublicationType.Book,
            [],
            "John Doe",
            DateOnly.Parse("2023-01-01"),
            "",
            "ABCD123",
            string.Empty,
            string.Empty,
            AcquisitionMethod.Purchase,
            11,
            12,
            string.Empty,
            null
        );

        // Act
        Publication savedPublication = await handler.Handle(command, default);

        // Assert
        Assert.NotNull(savedPublication);
        Assert.Equal(publication.Title, savedPublication.Title);
        await publicationRepository.Received(1).AddAsync(Arg.Is<Publication>(x => x.Title == publication.Title), Arg.Any<CancellationToken>());
        await authorRepository.Received(1).ListAllAsync(Arg.Any<Expression<Func<Author, bool>>>(), Arg.Any<CancellationToken>());
        await publisherRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == command.PublisherId), Arg.Any<CancellationToken>());
    }
}
