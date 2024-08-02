using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record UpdateMemberPayload
    : Payload<Member>
{
    public UpdateMemberPayload(Core.Domain.Primitives.Result<Member> result)
    : base(
        result,
        result.IsSuccess ?
        $"Member {result.Value?.FullName} updated successfully." :
        $"Member update failed"
    )
    { }
}
