using Microsoft.AspNetCore.Mvc;
using Kathanika.Domain.Exceptions;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed class Mutations
{
    [Error<InvalidFieldException>]
    public async Task<Author> AddAuthor([FromServices]IMediator mediator, AddAuthorCommand input)
    {
        var author = await mediator.Send(input);
        return author;
    }

    [Error<InvalidFieldException>]
    [Error<NotFoundWithTheIdException>]
    public async Task<Author> UpdateAuthor([FromServices]IMediator mediator, UpdateAuthorCommand input)
    {
        var author = await mediator.Send(input);
        return author;
    }

    [Error<NotFoundWithTheIdException>]
    public async Task DeleteAuthor([FromServices]IMediator mediator, string id)
    {
        await mediator.Send(new DeleteAuthorCommand(id));
    }
}
