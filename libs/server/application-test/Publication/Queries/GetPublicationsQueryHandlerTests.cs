using Kathanika.Application.Features.Publications.Queries;

namespace Kathanika.Application.Test.Queries;

public class GetPublicationsQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Call_AsQueryable()
    {
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        GetPublicationsQuery query = new();
        GetPublicationsQueryHandler handler = new(publicationRepository);

        IQueryable<Publication> queryable = await handler.Handle(query, default);

        Assert.NotNull(queryable);
        publicationRepository.Received(1).AsQueryable();
    }
}
