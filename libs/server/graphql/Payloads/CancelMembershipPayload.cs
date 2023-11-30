using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class CancelMembershipPayload(Member member) : Payload($"Member {member.FullName}'s membership status cancelled successfully.")
{
}
