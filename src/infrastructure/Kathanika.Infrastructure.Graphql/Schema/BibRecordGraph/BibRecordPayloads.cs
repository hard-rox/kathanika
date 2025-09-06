using Kathanika.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.BibRecordGraph;

public sealed record BookQuickAddPayload
    : Payload<BibRecord>
{
    public BookQuickAddPayload(KnResult<BibRecord> knResult)
        : base(knResult,
            knResult.IsSuccess
                ? $"New Bib record for book {knResult.Value.Title} created successfully. Bib items are creating."
                : "New book record creation failed.")
    {
    }
}