using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class PublisherMutations
{
    public async Task<AddPublisherPayload> AddPublisherAsync(
        [Service] IMediator mediator,
        AddPublisherCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Publisher> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<UpdatePublisherPayload> UpdatePublisherAsync(
        [Service] IMediator mediator,
        UpdatePublisherCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Publisher> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<DeletePublisherPayload> DeletePublisherAsync(
        [Service] IMediator mediator,
        string id,
        CancellationToken cancellationToken
    )
    {
        Result result = await mediator.Send(new DeletePublisherCommand(id), cancellationToken);
        return new(id, result);
    }
}
