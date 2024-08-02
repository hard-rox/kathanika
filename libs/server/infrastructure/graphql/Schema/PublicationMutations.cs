using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class PublicationMutations
{
    public async Task<AcquirePublicationPayload> AcquirePublicationAsync(
        [Service] IMediator mediator,
        AcquirePublicationCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Publication> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<UpdatePublicationPayload> UpdatePublicationAsync(
        [Service] IMediator mediator,
        UpdatePublicationCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Publication> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }
}
