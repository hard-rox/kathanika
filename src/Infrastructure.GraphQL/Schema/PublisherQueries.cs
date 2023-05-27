using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Queries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Publisher>> GetPublishersAsync([FromServices] IMediator mediator)
    {
        var publishers = await mediator.Send(new GetPublishersQuery());
        return publishers;
    }

    public async Task<Publisher> GetPublisherAsync([FromServices] IMediator mediator, string id)
    {
        var publisher = await mediator.Send(new GetPublisherByIdQuery(id));
        return publisher;
    }
}
