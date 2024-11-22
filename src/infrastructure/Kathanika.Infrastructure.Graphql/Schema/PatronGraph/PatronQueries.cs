using HotChocolate.Resolvers;
using Kathanika.Application.Features.Patrons.Queries;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Kathanika.Infrastructure.Graphql.Schema.PatronGraph;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class PatronQueries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Domain.Aggregates.PatronAggregate.Patron>> GetPatronsAsync(
            [Service] IMediator mediator,
            CancellationToken cancellationToken
        )
    {
        IQueryable<Domain.Aggregates.PatronAggregate.Patron> patrons = await mediator.Send(new GetPatronsQuery(), cancellationToken);
        return patrons;
    }

    public async Task<Domain.Aggregates.PatronAggregate.Patron?> GetPatronAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Domain.Primitives.Result<Domain.Aggregates.PatronAggregate.Patron> result = await mediator.Send(new GetPatronByIdQuery(id), cancellationToken);
        return result.Match(context);
    }
}