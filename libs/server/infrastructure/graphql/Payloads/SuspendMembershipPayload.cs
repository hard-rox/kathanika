using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record SuspendMembershipPayload
    : Payload
{
    public SuspendMembershipPayload(Member? Member) : base(
        Member is not null ?
        $"Member {Member.FullName}'s membership status suspended successfully." :
        $"Membership suspension failed."
    )
    {

    }
}
