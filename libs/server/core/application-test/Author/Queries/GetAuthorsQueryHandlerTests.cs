using Kathanika.Core.Application.Features.Authors.Queries;

namespace Kathanika.Core.Application.Test.Queries;

public class GetAuthorsQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_AsQueryable()
    {
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        GetAuthorsQuery query = new();
        GetAuthorsQueryHandler handler = new(authorRepository);

        IQueryable<Author> queryable = await handler.Handle(query, default);

        Assert.NotNull(queryable);
        authorRepository.Received(1).AsQueryable();
    }
}
