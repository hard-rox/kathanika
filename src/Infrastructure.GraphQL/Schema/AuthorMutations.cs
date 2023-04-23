using Microsoft.AspNetCore.Mvc;
using Kathanika.Domain.Exceptions;
using Kathanika.Infrastructure.GraphQL.Payloads;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Mutations
{
    [Error<InvalidFieldException>]
    public async Task<AddAuthorPayload> AddAuthorAsync([FromServices]IMediator mediator, AddAuthorCommand input)
    {
        var author = await mediator.Send(input);
        return new(author);
    }

    [Error<InvalidFieldException>]
    [Error<NotFoundWithTheIdException>]
    public async Task<UpdateAuthorPayload> UpdateAuthorAsync([FromServices]IMediator mediator, UpdateAuthorCommand input)
    {
        var author = await mediator.Send(input);
        return new(author);
    }

    [Error<NotFoundWithTheIdException>]
    public async Task<DeleteAuthorPayload> DeleteAuthorAsync([FromServices]IMediator mediator, string id)
    {
        await mediator.Send(new DeleteAuthorCommand(id));
        return new(id);
    }
}
