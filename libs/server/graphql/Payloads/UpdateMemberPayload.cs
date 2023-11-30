using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class UpdateMemberPayload(Member member) : Payload<Member>($"Author {member.FullName} updated successfully.", member)
{
}
