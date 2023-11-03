using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class MembershipStatusChangedPayload : Payload
{
    public MembershipStatusChangedPayload(Member member)
        : base($"Member {member.FullName}'s membership status changed to {member.Status} successfully")
    {
    }
}
