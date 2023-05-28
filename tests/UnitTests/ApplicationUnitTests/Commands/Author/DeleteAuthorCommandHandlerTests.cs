using System.Linq.Expressions;
using Kathanika.Application.Commands;
using Kathanika.Domain.Aggregates;
using Kathanika.Domain.Exceptions;
using Moq;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class DeleteAuthorCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_DeleteAsync()
    {
        var id = Guid.NewGuid().ToString();
        var author = Author.Create(
            "John",
            "Doe",
            DateTime.MinValue,
            null,
            "USA",
            ""
        );
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(author);
        authorRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<string>()))
            .Verifiable();
        var publicationRepositoryMock = new Mock<IPublicationRepository>();
        publicationRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Publication, bool>>>()))
            .ReturnsAsync(0);
        var command = new DeleteAuthorCommand(id);
        var handler = new DeleteAuthorCommandHandler(authorRepositoryMock.Object, publicationRepositoryMock.Object);

        await handler.Handle(command, default);

        authorRepositoryMock.Verify(x => x.DeleteAsync(It.Is<string>(x => x == id)), Times.Exactly(1));
    }

    [Fact]
    public async Task Handler_Should_Thorw_Exception_On_Invalid_Author_Id()
    {
        var id = Guid.NewGuid().ToString();
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<string>()))
            .Verifiable();
        var publicationRepositoryMock = new Mock<IPublicationRepository>();
        var command = new DeleteAuthorCommand(id);
        var handler = new DeleteAuthorCommandHandler(authorRepositoryMock.Object, publicationRepositoryMock.Object);

        var exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => { await handler.Handle(command, default); });

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
    }

    [Fact]
    public async Task Handler_Should_Thorw_Exception_When_Author_Has_Publication()
    {
        var id = Guid.NewGuid().ToString();
        var author = Author.Create(
            "John",
            "Doe",
            DateTime.MinValue,
            null,
            "USA",
            ""
        );
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(author);
        authorRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<string>()))
            .Verifiable();
        var publicationRepositoryMock = new Mock<IPublicationRepository>();
        publicationRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Publication, bool>>>()))
            .ReturnsAsync(1);
        var command = new DeleteAuthorCommand(id);
        var handler = new DeleteAuthorCommandHandler(authorRepositoryMock.Object, publicationRepositoryMock.Object);

        var exception = await Assert.ThrowsAsync<DeletionFailedException>(async () => { await handler.Handle(command, default); });

        Assert.IsAssignableFrom<DeletionFailedException>(exception);
    }

    [Fact]
    public async Task Handler_Should_Check_AuthorExistence_And_Publication()
    {
        var id = Guid.NewGuid().ToString();
        var author = Author.Create(
            "John",
            "Doe",
            DateTime.MinValue,
            null,
            "USA",
            ""
        );
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(author).Verifiable();
        authorRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<string>()))
            .Verifiable();
        var publicationRepositoryMock = new Mock<IPublicationRepository>();
        publicationRepositoryMock.Setup(x => x.CountAsync(It.IsAny<Expression<Func<Publication, bool>>>()))
            .ReturnsAsync(0).Verifiable();
        var command = new DeleteAuthorCommand(id);
        var handler = new DeleteAuthorCommandHandler(authorRepositoryMock.Object, publicationRepositoryMock.Object);

        await handler.Handle(command, default);

        authorRepositoryMock.Verify(x => x.GetByIdAsync(It.Is<string>(x => x == id)), Times.Exactly(1));
        publicationRepositoryMock.Verify(x => x.CountAsync(It.IsAny<Expression<Func<Publication, bool>>>()), Times.Exactly(1));
    }
}