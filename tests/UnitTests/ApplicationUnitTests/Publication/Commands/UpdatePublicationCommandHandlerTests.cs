using Kathanika.Application.Features.Publications.Commands;
using Kathanika.Domain.Exceptions;
using System.Linq.Expressions;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class UpdatePublicationCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Throw_Exception_On_Invalid_PublicationId()
    {
        var publicationId = Guid.NewGuid().ToString();
        var authorRepository = Substitute.For<IAuthorRepository>();
        var publicationRepository = Substitute.For<IPublicationRepository>();
        var command = new UpdatePublicationCommand(publicationId, new UpdatePublicationCommand.PublicationPatch(
            "", "", PublicationType.Book, "", null, null, null, "" ///TODO: Fix up update in domain...
        ));
        var handler = new UpdatePublicationCommandHandler(publicationRepository, authorRepository);

        var exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => await handler.Handle(command, default));

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
        Assert.Equal(publicationId, exception.Id);
    }

    [Fact]
    public async Task Handler_Should_Call_UpdateAsync_With_Updated_Publication()
    {
        var publicationId = Guid.NewGuid().ToString();
        var publication = Publication.Create(
            "Title",
            null,
            PublicationType.Book,
            "",
            DateOnly.MinValue,
            (decimal)10.2,
            2,
            "ABCD",
            new List<Author>()
            );
        var publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(publication);
        var authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.ListAllAsync(Arg.Any<Expression<Func<Author, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(new List<Author>());
        var command = new UpdatePublicationCommand(publicationId, new UpdatePublicationCommand.PublicationPatch(
            "Updated Title", "", PublicationType.Book, "", null, null, null, "" ///TODO: Fix up update in domain...
        ));
        var handler = new UpdatePublicationCommandHandler(publicationRepository, authorRepository);

        var updatedPublication = await handler.Handle(command, default);

        Assert.NotNull(updatedPublication);
        Assert.Equal("Updated Title", updatedPublication.Title);
        await publicationRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == publicationId), Arg.Any<CancellationToken>());
        await publicationRepository.Received(1).UpdateAsync(Arg.Is<Publication>(x => x == publication), Arg.Any<CancellationToken>());
    }
}