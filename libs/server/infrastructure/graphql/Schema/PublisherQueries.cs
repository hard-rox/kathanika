using HotChocolate.Resolvers;
using Kathanika.Core.Application.Features.Publishers.Queries;

namespace Kathanika.Infrastructure.Graphql.Schema;

public sealed partial class Queries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Publisher>> GetPublishersAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Publisher> publishers
            = await mediator.Send(new GetPublishersQuery(), cancellationToken);
        return publishers;
    }

    public async Task<Publisher?> GetPublisherAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Publisher> result
            = await mediator.Send(new GetPublisherByIdQuery(id), cancellationToken);
        return result.Match(context);
    }
}
