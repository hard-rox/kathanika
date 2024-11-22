using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.PatronGraph;
public sealed record UpdatePatronPayload(Domain.Primitives.Result<Domain.Aggregates.PatronAggregate.Patron> Result)
    : Payload<Domain.Aggregates.PatronAggregate.Patron>(Result, $"Patron {Result.Value.FullName} updated successfully.");