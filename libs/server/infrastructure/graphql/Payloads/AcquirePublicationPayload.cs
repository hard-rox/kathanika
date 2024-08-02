using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record AcquirePublicationPayload
    : Payload<Publication>
{
    public AcquirePublicationPayload(Core.Domain.Primitives.Result<Publication> result)
        : base(result, result.IsSuccess ?
    $"New publication {result.Value?.Title} added successfully." :
    $"New publication add failed.")
    { }
}
