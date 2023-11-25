using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class UpdateMemberPayload(Member member) : Payload<Member>($"Author {member.FullName} updated successfully.", member)
{
}
