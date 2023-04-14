using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public class Queries
{
    public async Task<IEnumerable<Author>> GetAuthors([FromServices] IMediator mediator)
    {
        var authors = await mediator.Send(new GetAuthorsQuery());
        return authors;
    }
}
