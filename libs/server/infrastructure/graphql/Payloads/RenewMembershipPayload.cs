using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record RenewMembershipPayload
    : Payload
{
    public RenewMembershipPayload(Core.Domain.Primitives.Result<Member> result) : base(
        result,
    result.IsSuccess ?
    $"Member {result.Value?.FullName}'s membership status renewed successfully." :
    $"Membership status renewal failed."
)
    {
    }
}
