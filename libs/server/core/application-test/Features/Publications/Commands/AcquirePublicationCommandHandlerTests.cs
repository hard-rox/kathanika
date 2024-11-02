using Kathanika.Core.Application.Features.Publications.Commands;

namespace Kathanika.Core.Application.Test.Features.Publications.Commands;

public class AcquirePublicationCommandHandlerTests
{
    private readonly IPublicationRepository publicationRepository;

    public AcquirePublicationCommandHandlerTests()
    {
        publicationRepository = Substitute.For<IPublicationRepository>();
    }

    [Fact]
    public async Task Handler_ShouldReturnSavedPublication_OnValidInput()
    {
        // Arrange
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
            12,
            string.Empty,
            string.Empty
        ).Value;

        AcquirePublicationCommandHandler handler = new(publicationRepository);
        publicationRepository.AddAsync(Arg.Any<Publication>(), Arg.Any<CancellationToken>())
            .Returns(publication);

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
    }
}
