using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

public record BookRecordCreatedEvent(
    string BibRecordId,
    int? NumberOfCopies) : IDomainEvent;