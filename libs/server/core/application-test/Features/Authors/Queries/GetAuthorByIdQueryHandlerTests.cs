using Kathanika.Core.Application.Features.Authors.Queries;

namespace Kathanika.Core.Application.Test.Features.Authors.Queries;

public class GetAuthorByIdQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Return_Author_With_Specific_Id()
    {
        Author author = Author.Create(
            "First Name",
            "Last Name",
            DateOnly.Parse("2000-01-01"),
            null,
            "",
            ""
        ).Value;
        string id = Guid.NewGuid().ToString();
        GetAuthorByIdQuery query = new(id);
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(author);
        GetAuthorByIdQueryHandler handler = new(authorRepository);

        Result<Author> result = await handler.Handle(query, default);

        await authorRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(author.FirstName, result.Value.FirstName);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenAuthorNotFound()
    {
        // Arrange
        GetAuthorByIdQuery query = new(Guid.NewGuid().ToString());
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Author?>(null));
        GetAuthorByIdQueryHandler handler = new(authorRepository);

        // Act
        Result<Author> result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(AuthorAggregateErrors.NotFound(query.Id), result.Errors);
    }
}
