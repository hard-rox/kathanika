using Kathanika.Application.Commands;
using Kathanika.Domain.Aggregates;
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
        authorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(author);
        authorRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Author>())).Verifiable();
        var command = new MarkAuthorAsDeceasedCommand("", dateOfDeath);
        var handler = new MarkAuthorAsDeceasedCommandHandler(authorRepositoryMock.Object);

        var updatedAuthor = await handler.Handle(command, default);

        Assert.NotNull(updatedAuthor);
        Assert.NotNull(updatedAuthor.DateOfDeath);
        Assert.Equal(author.DateOfDeath, dateOfDeath);
    }
}
