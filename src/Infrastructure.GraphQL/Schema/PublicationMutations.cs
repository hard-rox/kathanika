using Kathanika.Domain.Exceptions;
using Kathanika.Infrastructure.GraphQL.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Mutations
{
    [Error<InvalidFieldException>]
    public async Task<AddPublicationPayload> AddPublicationAsync([FromServices] IMediator mediator, AddPublicationCommand input)
    {
        var publication = await mediator.Send(input);
        return new AddPublicationPayload(publication);
    }
}
