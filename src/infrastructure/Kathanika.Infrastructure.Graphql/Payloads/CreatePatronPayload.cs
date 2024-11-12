using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;
public sealed record CreatePatronPayload
    : Payload<Patron>
{
    public CreatePatronPayload(Domain.Primitives.Result<Patron> result) : base(
       result,
       result.IsSuccess ?
       $"New patron {result.Value?.FullName} added successfully." :
       $"New patron add failed."
   )
    { }
}