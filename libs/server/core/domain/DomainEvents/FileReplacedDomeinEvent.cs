using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.DomainEvents;

public sealed record FileReplacedDomainEvent(string OldFileId, string NewFileId) : IDomainEvent;
