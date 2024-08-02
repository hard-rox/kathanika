using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record AddPublisherPayload
    : Payload<Publisher>
{
    public AddPublisherPayload(Core.Domain.Primitives.Result<Publisher> result)
    : base(result, result.IsSuccess ?
$"New publisher {result.Value?.Name} added successfully." :
$"New publisher add failed.")
    { }
}
