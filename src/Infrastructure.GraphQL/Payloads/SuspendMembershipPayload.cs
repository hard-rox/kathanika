using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class SuspendMembershipPayload : Payload
{
    public SuspendMembershipPayload(Member member)
        : base($"Member {member.FullName}'s membership status suspended successfully.")
    {
    }
}
