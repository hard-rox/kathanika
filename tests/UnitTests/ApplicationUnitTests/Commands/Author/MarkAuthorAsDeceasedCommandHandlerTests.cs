using Kathanika.Application.Commands;
using Kathanika.Domain.Aggregates;
using Kathanika.Domain.Exceptions;
using Moq;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class MarkAuthorAsDeceasedCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_UpdateAsync_With_Updated_Author_DateOfDeath()
    {
        var dateOfDeath = DateTime.Parse("2020-01-01");
        var author = Author.Create("John",
            "Doe",
            DateTime.MinValue,
            null,
            "",
            "");
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(author).Verifiable();
        authorRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Author>())).Verifiable();
        var command = new MarkAuthorAsDeceasedCommand("", dateOfDeath);
        var handler = new MarkAuthorAsDeceasedCommandHandler(authorRepositoryMock.Object);

        var updatedAuthor = await handler.Handle(command, default);

        Assert.NotNull(updatedAuthor);
        Assert.NotNull(updatedAuthor.DateOfDeath);
        Assert.Equal(author.DateOfDeath, dateOfDeath);
        authorRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Exactly(1));
        authorRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Author>(x => x == author)), Times.Exactly(1));
    }

    [Fact]
    public async Task Handler_Should_Throw_Exception_On_Invalid_AuthorId()
    {
        var authorId = Guid.NewGuid().ToString();
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        var command = new MarkAuthorAsDeceasedCommand(authorId, DateTime.MaxValue);
        var handler = new MarkAuthorAsDeceasedCommandHandler(authorRepositoryMock.Object);

        var exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => await handler.Handle(command, default));

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
        Assert.Equal(authorId, exception.Id);
    }
}
