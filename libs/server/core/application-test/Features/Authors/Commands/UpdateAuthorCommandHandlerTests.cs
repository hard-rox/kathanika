using Kathanika.Core.Application.Features.Authors.Commands;

namespace Kathanika.Core.Application.Test.Features.Authors.Commands;

public class UpdateAuthorCommandHandlerTests
{
    private readonly IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

    [Fact]
    public async Task Handler_ShouldThrowException_WhenInvalid_AuthorId()
    {
        string authorId = Guid.NewGuid().ToString();
        UpdateAuthorCommand command = new(authorId, new AuthorPatch());
        UpdateAuthorCommandHandler handler = new(authorRepository);

        NotFoundWithTheIdException exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => await handler.Handle(command, default));

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
        Assert.Equal(authorId, exception.Id);
    }

    [Fact]
    public async Task Handler_ShouldCallUpdateAsync_WithUpdatedAuthorDateOfDeath()
    {
        string authorId = Guid.NewGuid().ToString();
        Author author = Author.Create("John",
            "Doe",
            DateOnly.MinValue,
            null,
            "",
            "");
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(author);
        await authorRepository.UpdateAsync(Arg.Any<Author>(), Arg.Any<CancellationToken>());
        UpdateAuthorCommand command = new(authorId, new AuthorPatch(
            "Updated First Name",
            "Updated Last Name"
        ));
        UpdateAuthorCommandHandler handler = new(authorRepository);

        Author updatedAuthor = await handler.Handle(command, default);

        Assert.NotNull(updatedAuthor);
        Assert.Equal("Updated First Name", updatedAuthor.FirstName);
        Assert.Equal("Updated Last Name", updatedAuthor.LastName);
        await authorRepository.Received(1).GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        await authorRepository.Received(1).UpdateAsync(Arg.Is(author), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handler_ShouldHandleMarkAsDeceased_WhenMarkAsDeceased()
    {
        string authorId = Guid.NewGuid().ToString();
        Author author = Author.Create("John",
            "Doe",
            DateOnly.MinValue,
            null,
            "",
            "");
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(author);
        DateOnly dateOfDeath = DateOnly.FromDateTime(DateTime.UtcNow);
        UpdateAuthorCommand command = new(authorId, new AuthorPatch(
            "Updated First Name",
            "Updated Last Name",
            MarkedAsDeceased: true,
            DateOfDeath: dateOfDeath
        ));
        UpdateAuthorCommandHandler handler = new(authorRepository);

        Author updatedAuthor = await handler.Handle(command, default);

        Assert.NotNull(updatedAuthor);
        Assert.Equal(dateOfDeath, updatedAuthor.DateOfDeath);
    }
}
