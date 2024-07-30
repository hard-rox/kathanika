using HotChocolate.Resolvers;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class AuthorQueries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Author>> GetAuthorsAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Author> authors = await mediator.Send(new GetAuthorsQuery(), cancellationToken);
        return authors;
    }

    public async Task<Author?> GetAuthorAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Author> result = await mediator.Send(new GetAuthorByIdQuery(id), cancellationToken);
        return result.Match(context);
    }
}
