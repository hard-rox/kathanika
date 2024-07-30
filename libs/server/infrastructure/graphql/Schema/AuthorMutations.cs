using HotChocolate.Resolvers;
using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class AuthorMutations
{
    public async Task<AddAuthorPayload> AddAuthorAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        AddAuthorCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Author> result = await mediator.Send(input, cancellationToken);
        return result.Match<Author, AddAuthorPayload>(
            context,
            author => new(author),
            () => new(null)
        );
    }

    public async Task<UpdateAuthorPayload> UpdateAuthorAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        CancellationToken cancellationToken,
        UpdateAuthorCommand input
    )
    {
        Core.Domain.Primitives.Result<Author> result = await mediator.Send(input, cancellationToken);
        return result.Match<Author, UpdateAuthorPayload>(
            context,
            author => new(author),
            () => new(null)
        );
    }

    public async Task<DeleteAuthorPayload> DeleteAuthorAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        CancellationToken cancellationToken,
        string id
    )
    {
        Result result = await mediator.Send(new DeleteAuthorCommand(id), cancellationToken);
        return result.Match<DeleteAuthorPayload>(
            context,
            () => new(id, true),
            () => new(id, false)
        );
    }
}
