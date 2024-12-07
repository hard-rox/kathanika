using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.PatronGraph;

public sealed record UpdatePatronPayload
    : Payload<Patron>
{
    public UpdatePatronPayload(Domain.Primitives.Result<Patron> result) : base(result,
        $"Patron {result.Value.FullName} updated successfully.")
    {
    }
}