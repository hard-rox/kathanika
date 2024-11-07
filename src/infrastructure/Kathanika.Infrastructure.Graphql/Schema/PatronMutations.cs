using Kathanika.Application.Features.Patrons.Commands;
using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class PatronMutations
{
    public async Task<CreatePatronPayload> CreatePatronAsync(
      [Service] IMediator mediator,
      CancellationToken cancellationToken,
      CreatePatronCommand input
  )
    {
        Domain.Primitives.Result<Patron> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<UpdatePatronPayload> UpdatePatronAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        UpdatePatronCommand input
    )
    {
        Domain.Primitives.Result<Patron> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<DeletePatronPayload> DeletePatronAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        string id
    )
    {
        Result result = await mediator.Send(new DeletePatronCommand(id), cancellationToken);
        return new(id, result);
    }
}
