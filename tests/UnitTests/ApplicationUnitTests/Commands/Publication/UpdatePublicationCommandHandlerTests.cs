using Kathanika.Application.Commands;
using Kathanika.Domain.Exceptions;
using System.Linq.Expressions;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class UpdatePublicationCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Throw_Exception_On_Invalid_PublicationId()
    {
        var publicationId = Guid.NewGuid().ToString();
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        var publicationRepositoryMock = new Mock<IPublicationRepository>();
        var command = new UpdatePublicationCommand(publicationId, new UpdatePublicationCommand.PublicationPatch(
            "", "", PublicationType.Book, "", null, null, null, "" ///TODO: Fix up update in domain...
        ));
        var handler = new UpdatePublicationCommandHandler(publicationRepositoryMock.Object, authorRepositoryMock.Object);

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
        var publicationRepositoryMock = new Mock<IPublicationRepository>();
        publicationRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(publication)
            .Verifiable();
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(x => x.ListAllAsync(It.IsAny<Expression<Func<Author, bool>>>()))
            .ReturnsAsync(new List<Author>())
            .Verifiable();
        authorRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Author>())).Verifiable();
        var command = new UpdatePublicationCommand(publicationId, new UpdatePublicationCommand.PublicationPatch(
            "Updated Title", "", PublicationType.Book, "", null, null, null, "" ///TODO: Fix up update in domain...
        ));
        var handler = new UpdatePublicationCommandHandler(publicationRepositoryMock.Object, authorRepositoryMock.Object);

        var updatedPublication = await handler.Handle(command, default);

        Assert.NotNull(updatedPublication);
        Assert.Equal("Updated Title", updatedPublication.Title);
        publicationRepositoryMock.Verify(x => x.GetByIdAsync(It.Is<string>(x => x == publicationId)), Times.Exactly(1));
        publicationRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Publication>(x => x == publication)), Times.Exactly(1));
    }
}