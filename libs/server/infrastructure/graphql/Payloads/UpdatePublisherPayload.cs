using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record UpdatePublisherPayload
    : Payload<Publisher>
{
    public UpdatePublisherPayload(Core.Domain.Primitives.Result<Publisher> result)
    : base(
        result,
    result.IsSuccess ?
    $"Publisher {result.Value?.Name} updated successfully." :
    $"Publisher update failed.")
    { }
}
