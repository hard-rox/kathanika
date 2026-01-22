using Kathanika.Application.Features.QuickAdd.Commands;
using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Infrastructure.Graphql.Schema.BibRecordGraph;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global
[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class BibRecordMutations
{
    public async Task<BookQuickAddPayload> BookQuickAddAsync(
        [Service] IDispatcher dispatcher,
        BookQuickAddCommand input,
        CancellationToken cancellationToken
    )
    {
        KnResult<BibRecord> knResult = await dispatcher.Send(input, cancellationToken);
        return new BookQuickAddPayload(knResult);
    }
}