using Kathanika.Domain.Exceptions;
using Kathanika.Infrastructure.GraphQL.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Mutations
{
    [Error<InvalidFieldException>]
    public async Task<AddPublisherPayload> AddPublisherAsync([FromServices] IMediator mediator, AddPublisherCommand input)
    {
        var publisher = await mediator.Send(input);
        return new(publisher);
    }

    [Error<InvalidFieldException>]
    [Error<NotFoundWithTheIdException>]
    public async Task<UpdatePublisherPayload> UpdatePublisherAsync([FromServices] IMediator mediator, UpdatePublisherCommand input)
    {
        var publisher = await mediator.Send(input);
        return new(publisher);
    }

    [Error<NotFoundWithTheIdException>]
    [Error<DeletionFailedException>]
    public async Task<DeletePublisherCommand> DeletePublisherAsync([FromServices] IMediator mediator, string id)
    {
        await mediator.Send(new DeletePublisherCommand(id));
        return new(id);
    }
}
