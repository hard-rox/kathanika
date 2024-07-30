using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record RenewMembershipPayload
    : Payload
{
    public RenewMembershipPayload(Member? Member) : base(
    Member is not null ?
    $"Member {Member.FullName}'s membership status renewed successfully." :
    $"Membership status renewal failed."
)
    {

    }
}
