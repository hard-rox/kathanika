using Kathanika.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.BibRecordGraph;

public sealed record CreateBibRecordPayload
    : Payload<BibRecord>
{
    public CreateBibRecordPayload(KnResult<BibRecord> knResult)
        : base(knResult,
            knResult.IsSuccess
                ? $"New bib record with control number {knResult.Value.ControlNumber} created successfully."
                : "New bib record creation failed.")
    {
    }
}