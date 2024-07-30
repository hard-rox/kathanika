using HotChocolate.Resolvers;
using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class PublicationMutations
{
    public async Task<AcquirePublicationPayload> AcquirePublicationAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        AcquirePublicationCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Publication> result = await mediator.Send(input, cancellationToken);
        return result.Match<Publication, AcquirePublicationPayload>(
            context,
            publication => new(publication),
            () => new(null)
        );
    }

    public async Task<UpdatePublicationPayload> UpdatePublicationAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        UpdatePublicationCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Publication> result = await mediator.Send(input, cancellationToken);
        return result.Match<Publication, UpdatePublicationPayload>(
            context,
            publication => new(publication),
            () => new(null)
        );
    }
}
