using Kathanika.Application.Commands;
using Kathanika.Domain.Exceptions;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class UpdateAuthorCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Throw_Exception_On_Invalid_AuthorId()
    {
        var authorId = Guid.NewGuid().ToString();
        var authorRepository = Substitute.For<IAuthorRepository>();
        var command = new UpdateAuthorCommand(authorId, new UpdateAuthorCommand.AuthorPatch());
        var handler = new UpdateAuthorCommandHandler(authorRepository);

        var exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => await handler.Handle(command, default));

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
        Assert.Equal(authorId, exception.Id);
    }

    [Fact]
    public async Task Handler_Should_Call_UpdateAsync_With_Updated_Author_DateOfDeath()
    {
        var authorId = Guid.NewGuid().ToString();
        var author = Author.Create("John",
            "Doe",
            DateOnly.MinValue,
            null,
            "",
            "");
        var authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(author);
        await authorRepository.UpdateAsync(Arg.Any<Author>(), Arg.Any<CancellationToken>());
        var command = new UpdateAuthorCommand(authorId, new UpdateAuthorCommand.AuthorPatch(
            "Updated First Name",
            "Updated Last Name"
        ));
        var handler = new UpdateAuthorCommandHandler(authorRepository);

        var updatedAuthor = await handler.Handle(command, default);

        Assert.NotNull(updatedAuthor);
        Assert.Equal("Updated First Name", updatedAuthor.FirstName);
        Assert.Equal("Updated Last Name", updatedAuthor.LastName);
        await authorRepository.Received(1).GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        await authorRepository.Received(1).UpdateAsync(Arg.Is<Author>(x => x == author), Arg.Any<CancellationToken>());
    }
}