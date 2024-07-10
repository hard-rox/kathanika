using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.Graphql.Schema;

public sealed partial class Queries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public static async Task<IEnumerable<Publisher>> GetPublishersAsync([FromServices] IMediator mediator)
    {
        IQueryable<Publisher> publishers = await mediator.Send(new GetPublishersQuery());
        return publishers;
    }

    public static async Task<Publisher?> GetPublisherAsync([FromServices] IMediator mediator, string id)
    {
        Publisher publisher = await mediator.Send(new GetPublisherByIdQuery(id));
        return publisher;
    }
}
