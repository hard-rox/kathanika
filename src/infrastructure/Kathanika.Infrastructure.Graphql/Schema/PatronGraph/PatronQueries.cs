using HotChocolate.Resolvers;
using Kathanika.Application.Features.Patrons.Queries;
using Kathanika.Domain.Aggregates.PatronAggregate;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Kathanika.Infrastructure.Graphql.Schema.PatronGraph;

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
        KnResult<Patron> knResult = await mediator.Send(new GetPatronByIdQuery(id), cancellationToken);
        return knResult.Match(context);
    }
}