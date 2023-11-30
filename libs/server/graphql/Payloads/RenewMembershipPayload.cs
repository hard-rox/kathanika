using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class RenewMembershipPayload(Member member) : Payload($"Member {member.FullName}'s membership status renewed successfully.")
{
}
