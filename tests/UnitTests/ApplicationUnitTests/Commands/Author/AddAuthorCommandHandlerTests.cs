using Kathanika.Application.Commands;
using Kathanika.Domain.Aggregates;
using Moq;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public sealed class AddAuthorCommandHandlerTests
{
    private readonly Mock<IAuthorRepository> authorRepositoryMock;

    public AddAuthorCommandHandlerTests()
    {
        authorRepositoryMock = new();
    }

    [Fact]
    public async Task Handler_Should_Return_Author_On_SavingNewAuthor()
    {
        // Arrange
        var dummyAuthor = Author.Create(
            "Hello",
            "World",
            DateTime.Parse("2013-10-10"),
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
        var handler = new AddAuthorCommandHandler(authorRepositoryMock.Object);
        
        authorRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Author>()))
            .ReturnsAsync(Author.Create(
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
