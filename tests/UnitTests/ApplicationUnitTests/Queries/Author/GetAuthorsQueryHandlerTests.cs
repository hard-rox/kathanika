using Kathanika.Application.Queries;

namespace Kathanika.UnitTests.ApplicationUnitTests.Queries;

public class GetAuthorsQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_AsQueryable()
    {
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(x => x.AsQueryable())
            .Verifiable();
        var query = new GetAuthorsQuery();
        var handler = new GetAuthorsQueryHandler(authorRepositoryMock.Object);

        var queryable = await handler.Handle(query, default);

        Assert.NotNull(queryable);
        authorRepositoryMock.Verify(x => x.AsQueryable(), Times.Exactly(1));
    }
}