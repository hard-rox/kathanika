using Kathanika.Application.Features.Authors.Commands;
using Kathanika.Domain.Exceptions;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class MarkAuthorAsDeceasedCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_UpdateAsync_With_Updated_Author_DateOfDeath()
    {
        DateOnly dateOfDeath = DateOnly.Parse("2020-01-01");
        Author author = Author.Create("John",
            "Doe",
            DateOnly.MinValue,
            null,
            "",
            "");
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(author);
        MarkAuthorAsDeceasedCommand command = new("", dateOfDeath);
        MarkAuthorAsDeceasedCommandHandler handler = new(authorRepository);

        Author updatedAuthor = await handler.Handle(command, default);

        Assert.NotNull(updatedAuthor);
        Assert.NotNull(updatedAuthor.DateOfDeath);
        Assert.Equal(author.DateOfDeath, dateOfDeath);
        await authorRepository.Received(1).GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        await authorRepository.Received(1).UpdateAsync(Arg.Is<Author>(x => x == author), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handler_Should_Throw_Exception_On_Invalid_AuthorId()
    {
        string authorId = Guid.NewGuid().ToString();
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        MarkAuthorAsDeceasedCommand command = new(authorId, DateOnly.MaxValue);
        MarkAuthorAsDeceasedCommandHandler handler = new(authorRepository);

        NotFoundWithTheIdException exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => await handler.Handle(command, default));

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
        Assert.Equal(authorId, exception.Id);
    }
}
