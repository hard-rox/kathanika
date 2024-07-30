using Kathanika.Core.Application.Features.Authors.Commands;

namespace Kathanika.Core.Application.Test.Features.Authors.Commands;

public sealed class AddAuthorCommandHandlerTests
{
    private readonly IAuthorRepository authorRepository;

    public AddAuthorCommandHandlerTests()
    {
        authorRepository = Substitute.For<IAuthorRepository>();
    }

    [Fact]
    public async Task Handler_ShouldReturnAuthor_OnSavingNewAuthor()
    {
        // Arrange
        Author dummyAuthor = Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("2013-10-10"),
            null,
            "BD",
            "").Value;
        AddAuthorCommand command = new(
            dummyAuthor.FirstName,
            dummyAuthor.LastName,
            dummyAuthor.DateOfBirth,
            dummyAuthor.Nationality,
            dummyAuthor.Biography,
            dummyAuthor.DateOfDeath
            );
        AddAuthorCommandHandler handler = new(authorRepository);

        authorRepository.AddAsync(Arg.Any<Author>(), Arg.Any<CancellationToken>())
            .Returns(Author.Create(
                dummyAuthor.FirstName,
                dummyAuthor.LastName,
                dummyAuthor.DateOfBirth,
                dummyAuthor.DateOfDeath,
                dummyAuthor.Nationality,
                dummyAuthor.Biography).Value);

        // Act
        Result<Author> result = await handler.Handle(command, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(dummyAuthor.FirstName, result.Value.FirstName);
    }
}
