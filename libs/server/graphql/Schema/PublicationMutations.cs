using Kathanika.Domain.Exceptions;
using Kathanika.Infrastructure.GraphQL.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Mutations
{
    [Error<InvalidFieldException>]
    public async Task<AddPublicationPayload> AddPublicationAsync([FromServices] IMediator mediator, AddPublicationCommand input)
    {
        Publication publication = await mediator.Send(input);
        return new AddPublicationPayload(publication);
    }

    [Error<InvalidFieldException>]
    [Error<NotFoundWithTheIdException>]
    public async Task<UpdatePublicationPayload> UpdatePublicationAsync([FromServices] IMediator mediator, UpdatePublicationCommand input)
    {
        Publication publication = await mediator.Send(input);
        return new(publication);
    }
}
