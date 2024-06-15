using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class SuspendMembershipPayload(Member member) : Payload($"Member {member.FullName}'s membership status suspended successfully.")
{
}
