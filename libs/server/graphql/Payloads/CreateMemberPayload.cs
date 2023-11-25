using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class CreateMemberPayload(Member data) : Payload<Member>($"New member {data.FullName} added successfully.", data)
{
}
