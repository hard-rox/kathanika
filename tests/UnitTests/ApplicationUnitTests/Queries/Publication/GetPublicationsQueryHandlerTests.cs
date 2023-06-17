using Kathanika.Application.Queries;

namespace Kathanika.UnitTests.ApplicationUnitTests.Queries;

public class GetPublicationsQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_AsQueryable()
    {
        var publicationRepositoryMock = new Mock<IPublicationRepository>();
        publicationRepositoryMock.Setup(x => x.AsQueryable())
            .Verifiable();
        var query = new GetPublicationsQuery();
        var handler = new GetPublicationsQueryHandler(publicationRepositoryMock.Object);

        var queryable = await handler.Handle(query, default);

        Assert.NotNull(queryable);
        publicationRepositoryMock.Verify(x => x.AsQueryable(), Times.Exactly(1));
    }
}