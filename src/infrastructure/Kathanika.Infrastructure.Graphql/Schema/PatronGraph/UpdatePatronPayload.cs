using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.PatronGraph;

public sealed record UpdatePatronPayload
    : Payload<Patron>
{
    public UpdatePatronPayload(Domain.Primitives.KnResult<Patron> knResult) : base(knResult,
        $"Patron {knResult.Value.FullName} updated successfully.")
    {
    }
}