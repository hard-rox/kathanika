using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class RenewMembershipPayload(Member member) : Payload($"Member {member.FullName}'s membership status renewed successfully.")
{
}
