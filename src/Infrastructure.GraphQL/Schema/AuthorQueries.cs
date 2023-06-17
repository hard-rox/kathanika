using Kathanika.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Queries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Author>> GetAuthorsAsync(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        var authors = await mediator.Send(new GetAuthorsQuery(), cancellationToken);
        return authors;
    }

    public async Task<Author?> GetAuthorAsync([FromServices] IMediator mediator, string id, CancellationToken cancellationToken)
    {
        var author = await mediator.Send(new GetAuthorByIdQuery(id), cancellationToken);
        return author;
    }
}
