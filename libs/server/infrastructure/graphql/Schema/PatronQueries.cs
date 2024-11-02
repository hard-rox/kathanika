using HotChocolate.Resolvers;
using Kathanika.Core.Application.Features.Patrons.Queries;
using Kathanika.Core.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class PatronQueries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Patron>> GetPatronsAsync(
            [Service] IMediator mediator,
            CancellationToken cancellationToken
        )
    {
        IQueryable<Patron> patrons = await mediator.Send(new GetPatronsQuery(), cancellationToken);
        return patrons;
    }

    public async Task<Patron?> GetPatronAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Patron> result = await mediator.Send(new GetPatronByIdQuery(id), cancellationToken);
        return result.Match(context);
    }
}
