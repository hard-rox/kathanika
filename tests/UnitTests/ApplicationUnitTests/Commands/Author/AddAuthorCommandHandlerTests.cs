using Kathanika.Application.Commands;
using Kathanika.Domain.Aggregates;
using Kathanika.Domain.Exceptions;
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
        var dummyAuthor = new Author(
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
            .ReturnsAsync(new Author(
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

    [Fact]
    public async Task Handler_Should_Throw_InvalifFieldException_On_SameDateOfDeathAndDateOfBirth()
    {
        // Arrange
        var command = new AddAuthorCommand(
            "Hello",
            "World",
            DateTime.Parse("2013-10-10"),
            DateTime.Parse("2013-10-10"),
            "BD",
            ""
            );
        var handler = new AddAuthorCommandHandler(authorRepositoryMock.Object);

        // Act
        var ex = await Assert.ThrowsAsync<AggregateException>(async () => await handler.Handle(command, default));

        // Assert
        Assert.IsType<AggregateException>(ex);
        Assert.IsType<InvalidFieldException>(ex.InnerExceptions[0]);
        Assert.Equal(nameof(command.DateOfDeath), ((InvalidFieldException)ex.InnerExceptions[0]).FieldName);
    }

    [Fact]
    public async Task Handler_Should_Throw_InvalifFieldException_On_FutureDateOfDeath()
    {
        // Arrange
        var command = new AddAuthorCommand(
            "Hello",
            "World",
            DateTime.Parse("2013-10-10"),
            DateTime.Parse("2090-10-10"),
            "BD",
            ""
            );
        var handler = new AddAuthorCommandHandler(authorRepositoryMock.Object);

        // Act
        var ex = await Assert.ThrowsAsync<AggregateException>(async () => await handler.Handle(command, default));

        // Assert
        Assert.IsType<AggregateException>(ex);
        Assert.IsType<InvalidFieldException>(ex.InnerExceptions[0]);
        Assert.Equal(nameof(command.DateOfDeath), ((InvalidFieldException)ex.InnerExceptions[0]).FieldName);
    }

    [Fact]
    public async Task Handler_Should_Throw_InvalifFieldException_On_FutureDateOfBirth()
    {
        // Arrange
        var command = new AddAuthorCommand(
            "Hello",
            "World",
            DateTime.Parse("2090-10-10"),
            null,
            "BD",
            ""
            );
        var handler = new AddAuthorCommandHandler(authorRepositoryMock.Object);

        // Act
        var ex = await Assert.ThrowsAsync<AggregateException>(async () => await handler.Handle(command, default));

        // Assert
        Assert.IsType<AggregateException>(ex);
        Assert.IsType<InvalidFieldException>(ex.InnerExceptions[0]);
        Assert.Equal(nameof(command.DateOfBirth), ((InvalidFieldException)ex.InnerExceptions[0]).FieldName);
    }
}
