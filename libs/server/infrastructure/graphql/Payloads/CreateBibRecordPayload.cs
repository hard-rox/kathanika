using Kathanika.Core.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record CreateBibRecordPayload
    : Payload<BibRecord>
{
    public CreateBibRecordPayload(Core.Domain.Primitives.Result<BibRecord> result)
        : base(result, result.IsSuccess ?
    $"New bib record with control number {result.Value?.ControlNumber} created successfully." :
    $"New bib record creation failed.")
    { }
}
