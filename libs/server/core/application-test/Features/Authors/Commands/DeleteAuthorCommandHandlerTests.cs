using System.Linq.Expressions;
using Kathanika.Core.Application.Features.Authors.Commands;

namespace Kathanika.Core.Application.Test.Features.Authors.Commands;

public class DeleteAuthorCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_CallDeleteAsync()
    {
        string id = Guid.NewGuid().ToString();
        Author author = Author.Create(
            "John",
            "Doe",
            DateOnly.MinValue,
            null,
            "USA",
            ""
        ).Value;
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(author);
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        DeleteAuthorCommand command = new(id);
        DeleteAuthorCommandHandler handler = new(authorRepository, publicationRepository);

        await handler.Handle(command, default);

        await authorRepository.Received(1)
            .DeleteAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handler_ShouldReturnResultWithErrors_OnInvalidAuthorId()
    {
        string id = Guid.NewGuid().ToString();
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        DeleteAuthorCommand command = new(id);
        DeleteAuthorCommandHandler handler = new(authorRepository, publicationRepository);

        Result result = await handler.Handle(command, default);

        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Single(result.Errors);
        Assert.Equal(result.Errors[0].Code, AuthorAggregateErrors.NotFound(id).Code);
    }

    [Fact]
    public async Task Handler_ShouldReturnError_WhenAuthorHasPublications()
    {
        string id = Guid.NewGuid().ToString();
        Author author = Author.Create(
            "John",
            "Doe",
            DateOnly.MinValue,
            null,
            "USA",
            ""
        ).Value;
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(author);
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.CountAsync(Arg.Any<Expression<Func<Publication, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(1);
        DeleteAuthorCommand command = new(id);
        DeleteAuthorCommandHandler handler = new(authorRepository, publicationRepository);

        Result result = await handler.Handle(command, default);

        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Single(result.Errors);
        Assert.Equal(result.Errors[0].Code, AuthorAggregateErrors.HasPublication.Code);
    }

    [Fact]
    public async Task Handler_ShouldCheck_AuthorExistence_And_Publication()
    {
        string id = Guid.NewGuid().ToString();
        Author author = Author.Create(
            "John",
            "Doe",
            DateOnly.MinValue,
            null,
            "USA",
            ""
        ).Value;
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(author);
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.CountAsync(Arg.Any<Expression<Func<Publication, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(0);
        DeleteAuthorCommand command = new(id);
        DeleteAuthorCommandHandler handler = new(authorRepository, publicationRepository);

        await handler.Handle(command, default);

        await authorRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
        await publicationRepository.Received(1).CountAsync(Arg.Any<Expression<Func<Publication, bool>>>(), Arg.Any<CancellationToken>());
    }
}
