using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class UpdateMemberPayload(Member member) : Payload<Member>($"Author {member.FullName} updated successfully.", member)
{
}
