using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record UpdatePublicationPayload(Publication? Data)
: Payload<Publication>(
    Data is not null ?
    $"Publication {Data.Title} updated successfully." :
    $"Publication update failed.",
    Data
);
