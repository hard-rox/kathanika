using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.PatronGraph;
public sealed record CreatePatronPayload
    : Payload<Domain.Aggregates.PatronAggregate.Patron>
{
    public CreatePatronPayload(Domain.Primitives.Result<Domain.Aggregates.PatronAggregate.Patron> result) : base(
       result,
       result.IsSuccess ?
       $"New patron {result.Value.FullName} added successfully." :
       "New patron add failed."
   )
    { }
}