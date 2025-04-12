using Kathanika.Application.Features.Patrons.Commands;
using Kathanika.Domain.Aggregates.PatronAggregate;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Kathanika.Infrastructure.Graphql.Schema.PatronGraph;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class PatronMutations
{
    public async Task<CreatePatronPayload> CreatePatronAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        CreatePatronCommand input
    )
    {
        KnResult<Patron> knResult = await mediator.Send(input, cancellationToken);
        return new CreatePatronPayload(knResult);
    }

    public async Task<UpdatePatronPayload> UpdatePatronAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        UpdatePatronCommand input
    )
    {
        KnResult<Patron> knResult = await mediator.Send(input, cancellationToken);
        return new UpdatePatronPayload(knResult);
    }

    public async Task<DeletePatronPayload> DeletePatronAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        DeletePatronCommand input
    )
    {
        KnResult knResult = await mediator.Send(input, cancellationToken);
        return new DeletePatronPayload(input.Id, knResult);
    }
}