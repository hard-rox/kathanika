using Kathanika.Application.Publications.Queries;

namespace Kathanika.UnitTests.ApplicationUnitTests.Queries;

public class GetPublicationsQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_AsQueryable()
    {
        var publicationRepository = Substitute.For<IPublicationRepository>();
        var query = new GetPublicationsQuery();
        var handler = new GetPublicationsQueryHandler(publicationRepository);

        var queryable = await handler.Handle(query, default);

        Assert.NotNull(queryable);
        publicationRepository.Received(1).AsQueryable();
    }
}