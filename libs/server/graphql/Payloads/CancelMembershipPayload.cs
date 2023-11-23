using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class CancelMembershipPayload : Payload
{
    public CancelMembershipPayload(Member member)
        : base($"Member {member.FullName}'s membership status cancelled successfully.")
    {
    }
}
