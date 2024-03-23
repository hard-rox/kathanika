using Kathanika.Domain.Exceptions;
using Kathanika.GraphQL.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Kathanika.GraphQL.Schema;

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

    [Error<InvalidFieldException>]
    [Error<NotFoundWithTheIdException>]
    public async Task<PurchasePublicationPayload> PurchasePublicationAsync([FromServices] IMediator mediator, PurchasePublicationCommand input)
    {
        Publication publication = await mediator.Send(input);
        return new(publication);
    }
}
