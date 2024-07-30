using System.Linq.Expressions;
using Kathanika.Core.Application.Features.Publications.Commands;

namespace Kathanika.Core.Application.Test.Features.Publications.Commands;

public class UpdatePublicationCommandHandlerTests
{
    [Fact]
    public async Task Handler_ShouldReturnError_OnInvalidPublicationId()
    {
        string publicationId = Guid.NewGuid().ToString();
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        IPublisherRepository publisherRepository = Substitute.For<IPublisherRepository>();
        UpdatePublicationCommand command = new(publicationId, new PublicationPatch(
            "", "", PublicationType.Book, "", null, null, "", "", "", null
        ));
        UpdatePublicationCommandHandler handler = new(publicationRepository, authorRepository, publisherRepository);

        Result<Publication> result = await handler.Handle(command, default);

        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal(PublicationAggregateErrors.NotFound(publicationId).Code, result.Errors[0].Code);
    }

    [Fact]
    public async Task Handler_ShouldCallUpdateAsync_WithUpdatedPublication()
    {
        string publicationId = Guid.NewGuid().ToString();
        Publisher publisher = Publisher.Create(string.Empty).Value;
        Publication publication = Publication.Create(
            "Title",
            null,
            PublicationType.Book,
            DateOnly.MinValue,
            "",
            "ABCD",
            string.Empty,
            string.Empty,
            string.Empty,
            AcquisitionMethod.Purchase,
            10,
            publisher,
            11,
            string.Empty,
            null,
            []
        ).Value;
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(publication);
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        IPublisherRepository publisherRepository = Substitute.For<IPublisherRepository>();
        authorRepository.ListAllAsync(Arg.Any<Expression<Func<Author, bool>>>(), Arg.Any<CancellationToken>())
            .Returns([]);
        UpdatePublicationCommand command = new(publicationId, new PublicationPatch(
            "Updated Title", "", PublicationType.Book, "", null, null, null, "", "", "" //TODO: Fix up update in domain with non nullable...
        ));
        UpdatePublicationCommandHandler handler = new(publicationRepository, authorRepository, publisherRepository);

        Result<Publication> updatedPublicationResult = await handler.Handle(command, default);

        Assert.True(updatedPublicationResult.IsSuccess);
        Assert.NotNull(updatedPublicationResult.Value);
        Assert.Equal("Updated Title", updatedPublicationResult.Value.Title);
        await publicationRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == publicationId), Arg.Any<CancellationToken>());
        await publicationRepository.Received(1).UpdateAsync(Arg.Is<Publication>(x => x == publication), Arg.Any<CancellationToken>());
    }
}
