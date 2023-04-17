using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed class Mutations
{
    public async Task<Author> CreateAuthor([FromServices]IMediator mediator, CreateAuthorCommand input)
    {
        var author = await mediator.Send(input);
        return author;
    }

    public async Task<Author> UpdateAuthor([FromServices]IMediator mediator, UpdateAuthorCommand input)
    {
        var author = await mediator.Send(input);
        return author;
    }

    public async Task DeleteAuthor([FromServices]IMediator mediator, string id)
    {
        await mediator.Send(new DeleteAuthorCommand(id));
    }
}
