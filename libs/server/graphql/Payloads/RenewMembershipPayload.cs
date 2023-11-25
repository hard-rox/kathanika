using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class RenewMembershipPayload(Member member) : Payload($"Member {member.FullName}'s membership status renewed successfully.")
{
}
