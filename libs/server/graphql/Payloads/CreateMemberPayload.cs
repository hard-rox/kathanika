using Kathanika.GraphQL.Bases;

namespace Kathanika.GraphQL.Payloads;

public sealed class CreateMemberPayload(Member data) : Payload<Member>($"New member {data.FullName} added successfully.", data)
{
}
