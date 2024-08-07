using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record SuspendMembershipPayload
    : Payload
{
    public SuspendMembershipPayload(Core.Domain.Primitives.Result<Member> result) : base(
        result,
        result.IsSuccess ?
        $"Member {result.Value?.FullName}'s membership status suspended successfully." :
        $"Membership suspension failed."
    )
    {

    }
}
