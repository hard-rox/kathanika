using System.Linq.Expressions;
using Kathanika.Core.Application.Features.Publications.Commands;

namespace Kathanika.Core.Application.Test.Features.Publications.Commands;

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
    public async Task Handler_ShouldReturnSavedPublication_OnValidInput()
    {
        // Arrange
        Publisher publisher = Publisher.Create("John wick").Value;
        List<Author> authors = [
                Author.Create(
                    "John",
                    "Doe",
                    DateOnly.MinValue,
                    null,
                    "USA",
                    "A good writer"
                ).Value,
            Author.Create(
                    "Jane",
                    "Doe",
                    DateOnly.MinValue,
                    null,
                    "USA",
                    "Another good writer"
                ).Value
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
            string.Empty,
            AcquisitionMethod.Purchase,
            11,
            publisher,
            12,
            string.Empty,
            string.Empty,
            authors
        ).Value;

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
            string.Empty,
            AcquisitionMethod.Purchase,
            11,
            12,
            string.Empty,
            null
        );

        // Act
        Result<Publication> result = await handler.Handle(command, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(publication.Title, result.Value.Title);
        await publicationRepository.Received(1).AddAsync(Arg.Is<Publication>(x => x.Title == publication.Title), Arg.Any<CancellationToken>());
        await authorRepository.Received(1).ListAllAsync(Arg.Any<Expression<Func<Author, bool>>>(), Arg.Any<CancellationToken>());
        await publisherRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == command.PublisherId), Arg.Any<CancellationToken>());
    }
}
