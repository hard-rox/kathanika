using Kathanika.Application.Features.Authors.Commands;

namespace Kathanika.Application.Test.Commands;

public class UpdateAuthorCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Throw_Exception_On_Invalid_AuthorId()
    {
        string authorId = Guid.NewGuid().ToString();
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        UpdateAuthorCommand command = new(authorId, new UpdateAuthorCommand.AuthorPatch());
        UpdateAuthorCommandHandler handler = new(authorRepository);

        var exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => await handler.Handle(command, default));

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
        Assert.Equal(authorId, exception.Id);
    }

    [Fact]
    public async Task Handler_Should_Call_UpdateAsync_With_Updated_Author_DateOfDeath()
    {
        string authorId = Guid.NewGuid().ToString();
        Author author = Author.Create("John",
            "Doe",
            DateOnly.MinValue,
            null,
            "",
            "");
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(author);
        await authorRepository.UpdateAsync(Arg.Any<Author>(), Arg.Any<CancellationToken>());
        UpdateAuthorCommand command = new(authorId, new UpdateAuthorCommand.AuthorPatch(
            "Updated First Name",
            "Updated Last Name"
        ));
        UpdateAuthorCommandHandler handler = new(authorRepository);

        Author updatedAuthor = await handler.Handle(command, default);

        Assert.NotNull(updatedAuthor);
        Assert.Equal("Updated First Name", updatedAuthor.FirstName);
        Assert.Equal("Updated Last Name", updatedAuthor.LastName);
        await authorRepository.Received(1).GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        await authorRepository.Received(1).UpdateAsync(Arg.Is<Author>(x => x == author), Arg.Any<CancellationToken>());
    }
}
