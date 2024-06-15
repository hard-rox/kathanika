using Kathanika.Core.Domain.Exceptions;
using Kathanika.Infrastructure.Graphql.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.Graphql.Schema;

public sealed partial class Mutations
{
    [Error<InvalidFieldException>]
    public async Task<AcquirePublicationPayload> AcquirePublicationAsync([FromServices] IMediator mediator, AcquirePublicationCommand input)
    {
        Publication publication = await mediator.Send(input);
        return new AcquirePublicationPayload(publication);
    }

    [Error<InvalidFieldException>]
    [Error<NotFoundWithTheIdException>]
    public async Task<UpdatePublicationPayload> UpdatePublicationAsync([FromServices] IMediator mediator, UpdatePublicationCommand input)
    {
        Publication publication = await mediator.Send(input);
        return new(publication);
    }
}
