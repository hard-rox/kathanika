using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Queries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Author>> GetAuthors([FromServices] IMediator mediator)
    {
        var authors = await mediator.Send(new GetAuthorsQuery());
        return authors;
    }

    public async Task<Author?> GetAuthor([FromServices] IMediator mediator, string id)
    {
        var author = await mediator.Send(new GetAuthorByIdQuery(id));
        return author;
    }
}
