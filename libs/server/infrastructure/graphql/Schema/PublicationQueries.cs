using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.Graphql.Schema;

public sealed partial class Queries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public static async Task<IEnumerable<Publication>> GetPublicationsAsync([FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        IQueryable<Publication> publications = await mediator.Send(new GetPublicationsQuery(), cancellationToken);
        return publications;
    }

    public static async Task<Publication?> GetPublicationAsync([FromServices] IMediator mediator, string id, CancellationToken cancellationToken)
    {
        Publication? publication = await mediator.Send(new GetPublicationByIdQuery(id), cancellationToken);
        return publication;
    }
}
