// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

using HotChocolate.Resolvers;
using Kathanika.Application.Features.PurchaseOrders.Queries;
using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

namespace Kathanika.Infrastructure.Graphql.Schema.PurchaseOrderGraph;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class PurchaseOrderQueries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<PurchaseOrder>> GetPurchaseOrdersAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        IQueryable<PurchaseOrder> patrons = await mediator.Send(new GetPurchaseOrdersQuery(), cancellationToken);
        return patrons;
    }

    public async Task<PurchaseOrder?> GetPurchaseOrderAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        KnResult<PurchaseOrder> knResult = await mediator.Send(new GetPurchaseOrderByIdQuery(id), cancellationToken);
        return knResult.Match(context);
    }
}