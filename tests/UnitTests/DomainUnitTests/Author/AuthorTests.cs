using Kathanika.Domain.Aggregates;
using Kathanika.Domain.Exceptions;

namespace Kathanika.UnitTests.DomainUnitTests;

public sealed class AuthorTests
{
    [Fact]
    public void Create_Should_Throw_InvalidFieldException_On_SameDateOfDeathAndDateOfBirth()
    {
        // Arrange

        // Act
        var ex = Assert.Throws<AggregateException>(() =>
        {
            var command = Author.Create(
                        "Hello",
                        "World",
                        DateTime.Parse("2013-10-10"),
                        DateTime.Parse("2013-10-10"),
                        "BD",
                        ""
                        );
        });

        // Assert
        Assert.IsType<AggregateException>(ex);
        Assert.IsType<InvalidFieldException>(ex.InnerExceptions[0]);
        Assert.Equal("dateOfDeath", ((InvalidFieldException)ex.InnerExceptions[0]).FieldName);
    }

    [Fact]
    public void Create_Should_Throw_InvalidFieldException_On_FutureDateOfBirth()
    {
        // Arrange

        // Act
        var ex = Assert.Throws<AggregateException>(() =>
        {
            Author.Create(
            "Hello",
            "World",
            DateTime.Parse("2090-10-10"),
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
        var ex = Assert.Throws<AggregateException>(() =>
        {
            Author.Create(
            "Hello",
            "World",
            DateTime.Parse("2013-10-10"),
            DateTime.Parse("2090-10-10"),
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
        var author = Author.Create(
            "Hello",
            "World",
            DateTime.MinValue,
            null,
            "BD",
            ""
            );

        var updatedFirstName = "Updated Hello";

        // Act
        author.Update(updatedFirstName);

        // Assert
        Assert.Equal(updatedFirstName, author.FirstName);
    }

    [Fact]
    public void UpdateAuthor_Should_Throw_InvalidFieldException_On_FutureDateOfBirth()
    {
        // Arrange
        var author = Author.Create(
            "Hello",
            "World",
            DateTime.MinValue,
            null,
            "BD",
            ""
            );

        // Act
        InvalidFieldException ex = Assert.Throws<InvalidFieldException>(() => author.Update(dateOfBirth: DateTime.Parse("2090-01-01")));

        // Assert
        Assert.IsType<InvalidFieldException>(ex);
        Assert.Equal(nameof(author.DateOfBirth), ex.FieldName);
    }
}
