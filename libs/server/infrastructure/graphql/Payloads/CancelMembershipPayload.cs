using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class CancelMembershipPayload(Member member) : Payload($"Member {member.FullName}'s membership status cancelled successfully.")
{
}
