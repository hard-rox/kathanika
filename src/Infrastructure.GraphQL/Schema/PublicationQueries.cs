using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Queries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Publication>> GetPublicationsAsync([FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        var publications = await mediator.Send(new GetPublicationsQuery(), cancellationToken);
        return publications;
    }

    public async Task<Publication?> GetPublicationAsync([FromServices] IMediator mediator, string id, CancellationToken cancellationToken)
    {
        var publication = await mediator.Send(new GetPublicationByIdQuery(id), cancellationToken);
        return publication;
    }
}
