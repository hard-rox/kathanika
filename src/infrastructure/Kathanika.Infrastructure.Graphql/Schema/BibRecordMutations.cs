using Kathanika.Application.Features.BibRecords.Commands;
using Kathanika.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;
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
        Domain.Primitives.Result<BibRecord> result = await mediator.Send(input, cancellationToken);
        return new CreateBibRecordPayload(result);
    }
}