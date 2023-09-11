using System.Linq.Expressions;
using Kathanika.Application.Authors.Commands;
using Kathanika.Domain.Exceptions;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class DeleteAuthorCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_DeleteAsync()
    {
        var id = Guid.NewGuid().ToString();
        var author = Author.Create(
            "John",
            "Doe",
            DateOnly.MinValue,
            null,
            "USA",
            ""
        );
        var authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(author);
        var publicationRepository = Substitute.For<IPublicationRepository>();
        var command = new DeleteAuthorCommand(id);
        var handler = new DeleteAuthorCommandHandler(authorRepository, publicationRepository);

        await handler.Handle(command, default);

        await authorRepository.Received(1)
            .DeleteAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handler_Should_Throw_Exception_On_Invalid_Author_Id()
    {
        var id = Guid.NewGuid().ToString();
        var authorRepository = Substitute.For<IAuthorRepository>();
        var publicationRepository = Substitute.For<IPublicationRepository>();
        var command = new DeleteAuthorCommand(id);
        var handler = new DeleteAuthorCommandHandler(authorRepository, publicationRepository);

        var exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => { await handler.Handle(command, default); });

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
    }

    [Fact]
    public async Task Handler_Should_Throw_Exception_When_Author_Has_Publication()
    {
        var id = Guid.NewGuid().ToString();
        var author = Author.Create(
            "John",
            "Doe",
            DateOnly.MinValue,
            null,
            "USA",
            ""
        );
        var authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(author);
        var publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.CountAsync(Arg.Any<Expression<Func<Publication, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(1);
        var command = new DeleteAuthorCommand(id);
        var handler = new DeleteAuthorCommandHandler(authorRepository, publicationRepository);

        var exception = await Assert.ThrowsAsync<DeletionFailedException>(async () => { await handler.Handle(command, default); });

        Assert.IsAssignableFrom<DeletionFailedException>(exception);
    }

    [Fact]
    public async Task Handler_Should_Check_AuthorExistence_And_Publication()
    {
        var id = Guid.NewGuid().ToString();
        var author = Author.Create(
            "John",
            "Doe",
            DateOnly.MinValue,
            null,
            "USA",
            ""
        );
        var authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(author);
        var publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.CountAsync(Arg.Any<Expression<Func<Publication, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(0);
        var command = new DeleteAuthorCommand(id);
        var handler = new DeleteAuthorCommandHandler(authorRepository, publicationRepository);

        await handler.Handle(command, default);

        await authorRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
        await publicationRepository.Received(1).CountAsync(Arg.Any<Expression<Func<Publication, bool>>>(), Arg.Any<CancellationToken>());
    }
}