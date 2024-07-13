using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.MemberAggregate;

public sealed class MemberHasIssuedPublicationsException(IssuedPublication[] issuedPublications) : DomainException($"Member has issued {issuedPublications.Length} publications.")
{
    public IEnumerable<IssuedPublication> IssuedPublications { get; init; } = issuedPublications;
}
