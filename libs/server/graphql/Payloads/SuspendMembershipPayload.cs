using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class SuspendMembershipPayload(Member member) : Payload($"Member {member.FullName}'s membership status suspended successfully.")
{
}
