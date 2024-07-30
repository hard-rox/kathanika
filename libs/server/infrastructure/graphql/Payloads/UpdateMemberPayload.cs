using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record UpdateMemberPayload(Member? Member)
    : Payload<Member>(
        Member is not null ?
        $"Member {Member.FullName} updated successfully." :
        $"Member update failed",
        Member
    );
