using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;
public sealed record UpdatePatronPayload(Domain.Primitives.Result<Patron> Result)
    : Payload<Patron>(Result, $"Patron {Result.Value.FullName} updated successfully.");