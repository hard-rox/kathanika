using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

namespace Kathanika.Application.Features.PurchaseOrders.Queries;

public record GetPurchaseOrderByIdQuery(string Id) : IRequest<KnResult<PurchaseOrder>>;