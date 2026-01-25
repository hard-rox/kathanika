using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

namespace Kathanika.Application.Features.PurchaseOrders.Queries;

public sealed record GetPurchaseOrdersQuery : IQuery<IQueryable<PurchaseOrder>>;