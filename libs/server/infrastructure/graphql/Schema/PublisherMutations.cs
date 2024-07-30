using HotChocolate.Resolvers;
using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class PublisherMutations
{
    public async Task<AddPublisherPayload> AddPublisherAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        AddPublisherCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Publisher> result = await mediator.Send(input, cancellationToken);
        return result.Match<Publisher, AddPublisherPayload>(
            context,
            publisher => new(publisher),
            () => new(null)
        );
    }

    public async Task<UpdatePublisherPayload> UpdatePublisherAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        UpdatePublisherCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Publisher> result = await mediator.Send(input, cancellationToken);
        return result.Match<Publisher, UpdatePublisherPayload>(
            context,
            publisher => new(publisher),
            () => new(null)
        );
    }

    public async Task<DeletePublisherPayload> DeletePublisherAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Result result = await mediator.Send(new DeletePublisherCommand(id), cancellationToken);
        return result.Match<DeletePublisherPayload>(
            context,
            () => new(true, id),
            () => new(false, id)
        );
    }
}
