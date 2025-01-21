using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.PatronGraph;

public sealed record CreatePatronPayload
    : Payload<Patron>
{
    public CreatePatronPayload(KnResult<Patron> knResult) : base(
        knResult,
        knResult.IsSuccess ? $"New patron {knResult.Value.FullName} added successfully." : "New patron add failed."
    )
    {
    }
}