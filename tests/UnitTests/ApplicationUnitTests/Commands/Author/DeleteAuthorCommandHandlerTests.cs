using System.Linq.Expressions;
using Kathanika.Application.Commands;
using Kathanika.Domain.Aggregates;
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
}