using Kathanika.Core.Domain.Exceptions;

namespace Kathanika.Core.Domain.Test.AuthorAggregate;

public sealed class AuthorAggregateTests
{
    [Fact]
    public void Create_ShouldThrowInvalidFieldException_WhenFutureDateOfBirth()
    {
        // Arrange

        // Act
        AggregateException ex = Assert.Throws<AggregateException>(() =>
        {
            Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("2090-10-10"),
            null,
            "BD",
            ""
            );
        });

        // Assert
        Assert.IsType<AggregateException>(ex);
        Assert.IsType<InvalidFieldException>(ex.InnerExceptions[0]);
        Assert.Equal("dateOfBirth", ((InvalidFieldException)ex.InnerExceptions[0]).FieldName, true);
    }

    [Fact]
    public void Create_ShouldThrowInvalidFieldException_WhenFutureDateOfDeath()
    {
        // Arrange

        // Act
        AggregateException ex = Assert.Throws<AggregateException>(() =>
        {
            Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("2013-10-10"),
            DateOnly.Parse("2090-10-10"),
            "BD",
            ""
            );
        });

        // Assert
        Assert.IsType<AggregateException>(ex);
        Assert.IsType<InvalidFieldException>(ex.InnerExceptions[0]);
        Assert.Equal("dateOfDeath", ((InvalidFieldException)ex.InnerExceptions[0]).FieldName, true);
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
            );

        string updatedFirstName = "Updated Hello";

        // Act
        author.Update(updatedFirstName);

        // Assert
        Assert.Equal(updatedFirstName, author.FirstName);
    }

    [Fact]
    public void UpdateAuthor_ShouldThrowInvalidFieldException_WhenFutureDateOfBirth()
    {
        // Arrange
        Author author = Author.Create(
            "Hello",
            "World",
            DateOnly.MinValue,
            null,
            "BD",
            ""
            );

        // Act
        InvalidFieldException ex = Assert.Throws<InvalidFieldException>(() => author.Update(dateOfBirth: DateOnly.Parse("2090-01-01")));

        // Assert
        Assert.IsType<InvalidFieldException>(ex);
        Assert.Equal(nameof(author.DateOfBirth), ex.FieldName);
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
            );

        // Act
        author.MarkAsDeceased(dateOfDeath);

        // Assert
        Assert.Equal(author.DateOfDeath, dateOfDeath);
    }

    [Fact]
    public void MarkAsDeceased_ShouldThrowsInvalidFieldException_WhenFutureDateOfDeath()
    {
        // Arrange
        Author author = Author.Create(
            "Hello",
            "World",
            DateOnly.Parse("1993-01-01"),
            null,
            "BD",
            ""
            );

        // Act
        InvalidFieldException ex = Assert.Throws<InvalidFieldException>(
            () => author.MarkAsDeceased(DateOnly.Parse("2090-01-01")));

        // Assert
        Assert.IsType<InvalidFieldException>(ex);
        Assert.Equal(nameof(author.DateOfDeath), ex.FieldName);
    }

    [Fact]
    public void MarkAsDeceased_ShouldThrowInvalidFieldException_WhenInvalidDateOfDeath()
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
            );

        // Act
        InvalidFieldException ex = Assert.Throws<InvalidFieldException>(
            () => author.MarkAsDeceased(dateOfDeath));

        // Assert
        Assert.IsType<InvalidFieldException>(ex);
        Assert.Equal(nameof(author.DateOfDeath), ex.FieldName);
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
            );

        // Act
        author.UnmarkAsDeceased();

        // Assert
        Assert.Null(author.DateOfDeath);
    }
}
