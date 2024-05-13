using System.Linq.Expressions;
using Kathanika.Application.Features.Publications.Commands;

namespace Kathanika.Application.Test.Commands;

public class UpdatePublicationCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Throw_Exception_On_Invalid_PublicationId()
    {
        string publicationId = Guid.NewGuid().ToString();
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        UpdatePublicationCommand command = new(publicationId, new PublicationPatch(
            "", "", PublicationType.Book, "", null, null, "", "", "", null
        ));
        UpdatePublicationCommandHandler handler = new(publicationRepository, authorRepository);

        NotFoundWithTheIdException exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => await handler.Handle(command, default));

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
        Assert.Equal(publicationId, exception.Id);
    }

    [Fact]
    public async Task Handler_Should_Call_UpdateAsync_With_Updated_Publication()
    {
        string publicationId = Guid.NewGuid().ToString();
        Publication publication = Publication.Create(
            "Title",
            null,
            PublicationType.Book,
            "",
            DateOnly.MinValue,
            "",
            "ABCD",
            string.Empty,
            string.Empty,
            AcquisitionMethod.Purchase,
            10,
            11,
            string.Empty,
            null,
            []
            );
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(publication);
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.ListAllAsync(Arg.Any<Expression<Func<Author, bool>>>(), Arg.Any<CancellationToken>())
            .Returns([]);
        UpdatePublicationCommand command = new(publicationId, new PublicationPatch(
            "Updated Title", "", PublicationType.Book, "", null, null, "", "", "" //TODO: Fix up update in domain with non nullable...
        ));
        UpdatePublicationCommandHandler handler = new(publicationRepository, authorRepository);

        Publication updatedPublication = await handler.Handle(command, default);

        Assert.NotNull(updatedPublication);
        Assert.Equal("Updated Title", updatedPublication.Title);
        await publicationRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == publicationId), Arg.Any<CancellationToken>());
        await publicationRepository.Received(1).UpdateAsync(Arg.Is<Publication>(x => x == publication), Arg.Any<CancellationToken>());
    }
}
