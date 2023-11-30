using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class MemberHasIssuedPublicationsException(IssuedPublication[] issuedPublications) : DomainException($"Member has issued {issuedPublications.Length} publications.")
{
    public IEnumerable<IssuedPublication> IssuedPublications { get; init; } = issuedPublications;
}
