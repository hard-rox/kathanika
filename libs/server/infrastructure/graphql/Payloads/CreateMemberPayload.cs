using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed class CreateMemberPayload(Member data) : Payload<Member>($"New member {data.FullName} added successfully.", data)
{
}
