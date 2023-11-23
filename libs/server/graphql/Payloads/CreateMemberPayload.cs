using Kathanika.Infrastructure.GraphQL.Bases;

namespace Kathanika.Infrastructure.GraphQL.Payloads;

public sealed class CreateMemberPayload : Payload<Member>
{
    public CreateMemberPayload(Member data) : base($"New member {data.FullName} added successfully.", data)
    {
    }
}
