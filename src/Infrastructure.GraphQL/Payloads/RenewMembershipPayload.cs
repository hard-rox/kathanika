using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class RenewMembershipPayload : Payload
{
    public RenewMembershipPayload(Member member)
        : base($"Member {member.FullName}'s membership status renewed successfully.")
    {
    }
}
