using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class SuspendMembershipPayload(Member member) : Payload($"Member {member.FullName}'s membership status suspended successfully.")
{
}
