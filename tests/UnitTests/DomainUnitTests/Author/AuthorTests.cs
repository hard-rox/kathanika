using Kathanika.Domain.Aggregates;
using Kathanika.Domain.Exceptions;

namespace Kathanika.UnitTests.DomainUnitTests;

public sealed class AuthorTests
{
    [Fact]
    public void UpdateAuthor_Should_Return_UpdatedAuthor_On_ValidInput()
    {
        // Arrange
        var author = new Author(
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
        Assert.Equal( updatedFirstName, author.FirstName );
    }

    [Fact]
    public void UpdateAuthor_Should_Throw_InvalidFieldException_On_FutureDateOfBirth()
    {
        // Arrange
        var author = new Author(
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
        Assert.IsType<InvalidFieldException>( ex );
        Assert.Equal(nameof(author.DateOfBirth), ex.FieldName);
    }
}
