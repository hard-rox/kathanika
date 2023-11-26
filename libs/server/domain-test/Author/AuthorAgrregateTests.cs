using Kathanika.Domain.Aggregates;
using Kathanika.Domain.Exceptions;

namespace Kathanika.Domain.Test;

public sealed class AuthorAggregateTests
{
    [Fact]
    public void Create_Should_Throw_InvalidFieldException_On_FutureDateOfBirth()
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
    public void Handler_Should_Throw_InvalidFieldException_On_FutureDateOfDeath()
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
    public void UpdateAuthor_Should_Return_UpdatedAuthor_On_ValidInput()
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
    public void UpdateAuthor_Should_Throw_InvalidFieldException_On_FutureDateOfBirth()
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
    public void MarkAsDeceased_Should_Set_DateOfDeath_On_ValidDateOfDeath()
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
    public void MarkAsDeceased_Should_Throws_InvalidFieldException_On_FutureDateOfDeath()
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
    public void MarkAsDeceased_Should_Throws_InvalidFieldException_On_InvalidDateOfDeath()
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
    public void UnmarkAsDeceased_Should_Sets_DateOfDeath_Null()
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
