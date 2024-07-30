using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record CancelMembershipPayload
    : Payload
{
    public CancelMembershipPayload(Member? member) : base(
    member is not null ?
    $"Member {member.FullName}'s membership status cancelled successfully." :
    $"Membership status cancellation failed."
)
    {

    }
}
