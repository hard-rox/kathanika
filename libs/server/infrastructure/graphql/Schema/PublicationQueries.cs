using HotChocolate.Resolvers;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed partial class Queries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Publication>> GetPublicationsAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Publication> publications
            = await mediator.Send(new GetPublicationsQuery(), cancellationToken);
        return publications;
    }

    public async Task<Publication?> GetPublicationAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Publication> result
            = await mediator.Send(new GetPublicationByIdQuery(id), cancellationToken);
        return result.Match(context);
    }
}
