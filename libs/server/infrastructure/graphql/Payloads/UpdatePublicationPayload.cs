using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record UpdatePublicationPayload
: Payload<Publication>
{
    public UpdatePublicationPayload(Core.Domain.Primitives.Result<Publication> result)
    : base(
        result,
        result.IsSuccess ?
    $"Publication {result.Value?.Title} updated successfully." :
    $"Publication update failed."
)
    { }
}
