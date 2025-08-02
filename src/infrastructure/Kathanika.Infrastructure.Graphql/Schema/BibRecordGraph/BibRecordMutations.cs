using Kathanika.Application.Features.BibRecords.Commands;
using Kathanika.Application.Features.QuickAdd.Commands;
using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Infrastructure.Graphql.Schema.BibRecordGraph;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global
[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class BibRecordMutations
{
    public async Task<CreateBibRecordPayload> CreateBibRecordAsync(
        [Service] IMediator mediator,
        CreateBibRecordCommand input,
        CancellationToken cancellationToken
    )
    {
        KnResult<BibRecord> knResult = await mediator.Send(input, cancellationToken);
        return new CreateBibRecordPayload(knResult);
    }
    
    public async Task<BookQuickAddPayload> BookQuickAddAsync(
        [Service] IMediator mediator,
        BookQuickAddCommand input,
        CancellationToken cancellationToken
    )
    {
        KnResult<BibRecord> knResult = await mediator.Send(input, cancellationToken);
        return new BookQuickAddPayload(knResult);
    }
}