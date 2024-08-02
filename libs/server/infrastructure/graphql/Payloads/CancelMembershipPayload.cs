using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record CancelMembershipPayload
    : Payload
{
    public CancelMembershipPayload(Core.Domain.Primitives.Result<Member> result) : base(
        result,
    result.IsSuccess ?
    $"Member {result.Value?.FullName}'s membership status cancelled successfully." :
    $"Membership status cancellation failed."
)
    {

    }
}
