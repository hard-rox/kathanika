using Kathanika.Application.Authors.Commands;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public sealed class AddAuthorCommandHandlerTests
{
    private readonly IAuthorRepository authorRepository;

    public AddAuthorCommandHandlerTests()
    {
        authorRepository = Substitute.For<IAuthorRepository>();
    }

    [Fact]
    public async Task Handler_Should_Return_Author_On_SavingNewAuthor()
    {
        // Arrange
        var dummyAuthor = Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("2013-10-10"),
            null,
            "BD",
            "");
        var command = new AddAuthorCommand(
            dummyAuthor.FirstName, 
            dummyAuthor.LastName, 
            dummyAuthor.DateOfBirth, 
            dummyAuthor.DateOfDeath, 
            dummyAuthor.Nationality, 
            dummyAuthor.Biography
            );
        var handler = new AddAuthorCommandHandler(authorRepository);
        
        authorRepository.AddAsync(Arg.Any<Author>(), Arg.Any<CancellationToken>())
            .Returns(Author.Create(
                dummyAuthor.FirstName,
                dummyAuthor.LastName,
                dummyAuthor.DateOfBirth,
                dummyAuthor.DateOfDeath,
                dummyAuthor.Nationality,
                dummyAuthor.Biography));

        // Act
        var savedAuthor = await handler.Handle(command, default);

        // Assert
        Assert.NotNull(savedAuthor);
        Assert.Equal(dummyAuthor.FirstName, savedAuthor.FirstName);
    }
}
