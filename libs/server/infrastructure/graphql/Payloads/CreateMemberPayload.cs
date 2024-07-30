using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record CreateMemberPayload(Member? Data)
    : Payload<Member>(
        Data is not null ?
        $"New member {Data.FullName} created successfully." :
        $"New member creation failed.",
        Data
    );
