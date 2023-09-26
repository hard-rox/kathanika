using Kathanika.Application.Features.Authors.Commands;
using Kathanika.Domain.Exceptions;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class MarkAuthorAsDeceasedCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_UpdateAsync_With_Updated_Author_DateOfDeath()
    {
        var dateOfDeath = DateOnly.Parse("2020-01-01");
        var author = Author.Create("John",
            "Doe",
            DateOnly.MinValue,
            null,
            "",
            "");
        var authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(author);
        var command = new MarkAuthorAsDeceasedCommand("", dateOfDeath);
        var handler = new MarkAuthorAsDeceasedCommandHandler(authorRepository);

        var updatedAuthor = await handler.Handle(command, default);

        Assert.NotNull(updatedAuthor);
        Assert.NotNull(updatedAuthor.DateOfDeath);
        Assert.Equal(author.DateOfDeath, dateOfDeath);
        await authorRepository.Received(1).GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        await authorRepository.Received(1).UpdateAsync(Arg.Is<Author>(x => x == author), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handler_Should_Throw_Exception_On_Invalid_AuthorId()
    {
        var authorId = Guid.NewGuid().ToString();
        var authorRepository = Substitute.For<IAuthorRepository>();
        var command = new MarkAuthorAsDeceasedCommand(authorId, DateOnly.MaxValue);
        var handler = new MarkAuthorAsDeceasedCommandHandler(authorRepository);

        var exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => await handler.Handle(command, default));

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
        Assert.Equal(authorId, exception.Id);
    }
}
