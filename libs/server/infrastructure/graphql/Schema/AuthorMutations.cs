using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class AuthorMutations
{
    public async Task<AddAuthorPayload> AddAuthorAsync(
        [Service] IMediator mediator,
        AddAuthorCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Author> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<UpdateAuthorPayload> UpdateAuthorAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        UpdateAuthorCommand input
    )
    {
        Core.Domain.Primitives.Result<Author> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<DeleteAuthorPayload> DeleteAuthorAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        string id
    )
    {
        Result result = await mediator.Send(new DeleteAuthorCommand(id), cancellationToken);
        return new(id, result);
    }
}
