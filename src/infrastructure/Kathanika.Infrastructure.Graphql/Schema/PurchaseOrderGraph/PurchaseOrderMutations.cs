using Kathanika.Application.Features.PurchaseOrders.Commands;
using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Kathanika.Infrastructure.Graphql.Schema.PurchaseOrderGraph;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class PurchaseOrderMutations
{
    public async Task<CreatePurchaseOrderPayload> CreatePurchaseOrderAsync(
        [Service] IDispatcher dispatcher,
        CancellationToken cancellationToken,
        CreatePurchaseOrderCommand input
    )
    {
        KnResult<PurchaseOrder> knResult = await dispatcher.Send(input, cancellationToken);
        return new CreatePurchaseOrderPayload(knResult);
    }

    public async Task<UpdatePurchaseOrderPayload> UpdatePurchaseOrderAsync(
        [Service] IDispatcher dispatcher,
        CancellationToken cancellationToken,
        UpdatePurchaseOrderCommand input
    )
    {
        KnResult<PurchaseOrder> knResult = await dispatcher.Send(input, cancellationToken);
        return new UpdatePurchaseOrderPayload(knResult);
    }
    //
    // public async Task<DeletePurchaseOrderPayload> DeletePurchaseOrderAsync(
    //     [Service] IDispatcher dispatcher,
    //     CancellationToken cancellationToken,
    //     string id
    // )
    // {
    //     KnResult knResult = await dispatcher.Send(new DeletePurchaseOrderCommand(id), cancellationToken);
    //     return new DeletePurchaseOrderPayload(id, knResult);
    // }
}