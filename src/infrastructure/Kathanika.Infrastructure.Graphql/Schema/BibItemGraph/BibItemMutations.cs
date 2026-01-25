using Kathanika.Application.Features.BibItems.Commands;
using Kathanika.Domain.Aggregates.BibItemAggregate;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Kathanika.Infrastructure.Graphql.Schema.BibItemGraph;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class BibItemMutations
{
    public async Task<AddBibItemPayload> AddBibItemAsync(
        [Service] IDispatcher dispatcher,
        CancellationToken cancellationToken,
        AddBibItemCommand input
    )
    {
        KnResult<BibItem> knResult = await dispatcher.Send(input, cancellationToken);
        return new AddBibItemPayload(knResult);
    }

    public async Task<UpdateBibItemPayload> UpdateBibItemAsync(
        [Service] IDispatcher dispatcher,
        CancellationToken cancellationToken,
        UpdateBibItemCommand input
    )
    {
        KnResult<BibItem> knResult = await dispatcher.Send(input, cancellationToken);
        return new UpdateBibItemPayload(knResult);
    }

    public async Task<CheckOutBibItemPayload> CheckOutBibItemAsync(
        [Service] IDispatcher dispatcher,
        CancellationToken cancellationToken,
        CheckOutBibItemCommand input
    )
    {
        KnResult<BibItem> knResult = await dispatcher.Send(input, cancellationToken);
        return new CheckOutBibItemPayload(knResult);
    }

    public async Task<CheckInBibItemPayload> CheckInBibItemAsync(
        [Service] IDispatcher dispatcher,
        CancellationToken cancellationToken,
        CheckInBibItemCommand input
    )
    {
        KnResult<BibItem> knResult = await dispatcher.Send(input, cancellationToken);
        return new CheckInBibItemPayload(knResult);
    }

    public async Task<WithdrawBibItemPayload> WithdrawBibItemAsync(
        [Service] IDispatcher dispatcher,
        CancellationToken cancellationToken,
        WithdrawBibItemCommand input
    )
    {
        KnResult<BibItem> knResult = await dispatcher.Send(input, cancellationToken);
        return new WithdrawBibItemPayload(knResult);
    }
}