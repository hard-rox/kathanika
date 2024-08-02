using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record CreateMemberPayload
    : Payload<Member>
{
    public CreateMemberPayload(Core.Domain.Primitives.Result<Member> result) : base(
        result,
        result.IsSuccess ?
        $"New member {result.Value?.FullName} created successfully." :
        $"New member creation failed."
    )
    { }
}
