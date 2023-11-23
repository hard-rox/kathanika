using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class UpdateMemberPayload : Payload<Member>
{
    public UpdateMemberPayload(Member member) : base($"Author {member.FullName} updated successfully.", member)
    {
    }
}
