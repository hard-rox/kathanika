using Microsoft.AspNetCore.Mvc;
using Kathanika.Domain.Exceptions;
using Kathanika.Infrastructure.GraphQL.Payloads;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed class Mutations
{
    [Error<InvalidFieldException>]
    public async Task<AddAuthorPayload> AddAuthor([FromServices]IMediator mediator, AddAuthorCommand input)
    {
        var author = await mediator.Send(input);
        return new(author);
    }

    [Error<InvalidFieldException>]
    [Error<NotFoundWithTheIdException>]
    public async Task<UpdateAuthorPayload> UpdateAuthor([FromServices]IMediator mediator, UpdateAuthorCommand input)
    {
        var author = await mediator.Send(input);
        return new(author);
    }

    [Error<NotFoundWithTheIdException>]
    public async Task<DeleteAuthorPayload> DeleteAuthor([FromServices]IMediator mediator, string id)
    {
        await mediator.Send(new DeleteAuthorCommand(id));
        return new(id);
    }
}
