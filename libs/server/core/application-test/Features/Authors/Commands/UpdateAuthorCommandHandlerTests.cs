using Kathanika.Core.Application.Features.Authors.Commands;

namespace Kathanika.Core.Application.Test.Features.Authors.Commands;

public class UpdateAuthorCommandHandlerTests
{
    private readonly IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();

    [Fact]
    public async Task Handler_ShouldReturnError_WhenInvalidAuthorId()
    {
        string authorId = Guid.NewGuid().ToString();
        UpdateAuthorCommand command = new(authorId, new AuthorPatch());
        UpdateAuthorCommandHandler handler = new(authorRepository);

        Result<Author> result = await handler.Handle(command, default);

        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Single(result.Errors);
        Assert.Equal(AuthorAggregateErrors.NotFoundError(authorId).Code, result.Errors[0].Code);
    }

    [Fact]
    public async Task Handler_ShouldCallUpdateAsync_WithUpdatedAuthor()
    {
        string authorId = Guid.NewGuid().ToString();
        Author author = Author.Create("John",
            "Doe",
            DateOnly.MinValue,
            null,
            "",
            "").Value;
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(author);
        await authorRepository.UpdateAsync(Arg.Any<Author>(), Arg.Any<CancellationToken>());
        UpdateAuthorCommand command = new(authorId, new AuthorPatch(
            "Updated First Name",
            "Updated Last Name"
        ));
        UpdateAuthorCommandHandler handler = new(authorRepository);

        Result<Author> updatedAuthorResult = await handler.Handle(command, default);

        Assert.True(updatedAuthorResult.IsSuccess);
        Assert.NotNull(updatedAuthorResult.Value);
        Assert.Equal("Updated First Name", updatedAuthorResult.Value.FirstName);
        Assert.Equal("Updated Last Name", updatedAuthorResult.Value.LastName);
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
            "").Value;
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(author);
        DateOnly dateOfDeath = DateOnly.FromDateTime(DateTime.UtcNow);
        UpdateAuthorCommand command = new(authorId, new AuthorPatch(
            "Updated First Name",
            "Updated Last Name",
            MarkedAsDeceased: true,
            DateOfDeath: dateOfDeath
        ));
        UpdateAuthorCommandHandler handler = new(authorRepository);

        Result<Author> updatedAuthorResult = await handler.Handle(command, default);

        Assert.NotNull(updatedAuthorResult.Value);
        Assert.Equal(dateOfDeath, updatedAuthorResult.Value.DateOfDeath);
    }
}
