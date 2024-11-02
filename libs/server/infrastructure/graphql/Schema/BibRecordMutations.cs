using Kathanika.Core.Application.Features.BibRecords.Commands;
using Kathanika.Core.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class BibRecordMutations
{
    public async Task<CreateBibRecordPayload> CreateBibRecordAsync(
        [Service] IMediator mediator,
        CreateBibRecordCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<BibRecord> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }
}
