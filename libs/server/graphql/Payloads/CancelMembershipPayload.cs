using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class CancelMembershipPayload(Member member) : Payload($"Member {member.FullName}'s membership status cancelled successfully.")
{
}
