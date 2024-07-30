namespace Kathanika.Core.Domain.Test.AuthorAggregate;

public sealed class AuthorAggregateTests
{
    [Fact]
    public void Create_ShouldReturnError_WhenFutureDateOfBirth()
    {
        // Arrange

        // Act
        Result<Author> result = Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("2090-10-10"),
            null,
            "BD",
            ""
            );

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(AuthorAggregateErrors.FutureDateOfBirth, result.Errors[0]);
    }

    [Fact]
    public void Create_ShouldReturnError_WhenFutureDateOfDeath()
    {
        // Arrange

        // Act
        Result<Author> result = Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("2013-10-10"),
            DateOnly.Parse("2090-10-10"),
            "BD",
            ""
            );

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(AuthorAggregateErrors.FutureDateOfDeath, result.Errors[0]);
    }

    [Fact]
    public void UpdateAuthor_ShouldReturnUpdatedAuthor_WhenValidInput()
    {
        // Arrange
        Author author = Author.Create(
            "Hello",
            "World",
            DateOnly.MinValue,
            null,
            "BD",
            ""
            ).Value;

        string updatedFirstName = "Updated Hello";

        // Act
        Result result = author.Update(updatedFirstName);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(updatedFirstName, author.FirstName);
    }

    [Fact]
    public void UpdateAuthor_ShouldReturnError_WhenFutureDateOfBirth()
    {
        // Arrange
        Author author = Author.Create(
            "Hello",
            "World",
            DateOnly.MinValue,
            null,
            "BD",
            ""
            ).Value;

        // Act
        Result result = author.Update(dateOfBirth: DateOnly.Parse("2090-01-01"));

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(AuthorAggregateErrors.FutureDateOfBirth, result.Errors[0]);
    }

    [Fact]
    public void MarkAsDeceased_ShouldSetDateOfDeath_WhenValidDateOfDeath()
    {
        // Arrange
        DateOnly dateOfDeath = DateOnly.Parse("2000-01-01");
        Author author = Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("1993-01-01"),
            null,
            "BD",
            ""
            ).Value;

        // Act
        Result result = author.MarkAsDeceased(dateOfDeath);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(author.DateOfDeath, dateOfDeath);
    }

    [Fact]
    public void MarkAsDeceased_ShouldReturnError_WhenFutureDateOfDeath()
    {
        // Arrange
        Author author = Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("1993-01-01"),
            null,
            "BD",
            ""
            ).Value;

        // Act
        Result result = author.MarkAsDeceased(DateOnly.Parse("2090-01-01"));

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(AuthorAggregateErrors.FutureDateOfDeath, result.Errors[0]);
    }

    [Fact]
    public void MarkAsDeceased_ShouldReturnError_WhenInvalidDateOfDeath()
    {
        // Arrange
        DateOnly dateOfDeath = DateOnly.Parse("1990-01-01");
        Author author = Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("1993-01-01"),
            null,
            "BD",
            ""
            ).Value;

        // Act
        Result result = author.MarkAsDeceased(dateOfDeath);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotEmpty(result.Errors);
        Assert.Equal(AuthorAggregateErrors.DateOfBirthFollowingDateOfDeath, result.Errors[0]);
    }

    [Fact]
    public void UnmarkAsDeceased_ShouldSetDateOfDeathNull_WhenCalled()
    {
        // Arrange
        Author author = Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("1993-01-01"),
            DateOnly.Parse("2023-01-01"),
            "BD",
            ""
            ).Value;

        // Act
        Result result = author.UnmarkAsDeceased();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Null(author.DateOfDeath);
    }
}
