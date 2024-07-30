using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record AcquirePublicationPayload(Publication? Data)
    : Payload<Publication>(
        Data is not null ?
    $"New publication {Data.Title} added successfully." :
    $"New publication add failed.",
    Data
    );
