using Kathanika.Application.Features.Authors.Queries;

namespace Kathanika.UnitTests.ApplicationUnitTests.Queries;

public class GetAuthorsQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_AsQueryable()
    {
        var authorRepository = Substitute.For<IAuthorRepository>();
        var query = new GetAuthorsQuery();
        var handler = new GetAuthorsQueryHandler(authorRepository);

        var queryable = await handler.Handle(query, default);

        Assert.NotNull(queryable);
        authorRepository.Received(1).AsQueryable();
    }
}