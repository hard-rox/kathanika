using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.Member;

public sealed class MemberHasIssuedPublicationsException : DomainException
{
    public IEnumerable<IssuedPublication> IssuedPublications { get; init; }
    public MemberHasIssuedPublicationsException(IssuedPublication[] issuedPublications)
        : base($"Member has issued {issuedPublications.Length} publications.")
    {
        IssuedPublications = issuedPublications;
    }
}
