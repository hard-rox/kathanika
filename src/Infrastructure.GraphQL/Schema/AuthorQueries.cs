using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Queries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Author>> GetAuthorsAsync([FromServices] IMediator mediator)
    {
        var authors = await mediator.Send(new GetAuthorsQuery());
        return authors;
    }

    public async Task<Author> GetAuthorAsync([FromServices] IMediator mediator, string id)
    {
        var author = await mediator.Send(new GetAuthorByIdQuery(id));
        return author;
    }
}
