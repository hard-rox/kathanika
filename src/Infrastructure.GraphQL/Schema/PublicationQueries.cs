using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Queries
{
    [UsePaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Publication>> GetPublications([FromServices] IMediator mediator)
    {
        var publications = await mediator.Send(new GetPublicationsQuery());
        return publications;
    }

    //public async Task<Publication?> GetPublication([FromServices] IMediator mediator, string id)
    //{
    //    var publication = await mediator.Send(new GetAuthorByIdQuery(id));
    //    return publication;
    //}
}
