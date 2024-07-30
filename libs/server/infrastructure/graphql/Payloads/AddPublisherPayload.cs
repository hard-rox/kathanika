using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record AddPublisherPayload(Publisher? Data)
    : Payload<Publisher>(
        Data is not null ?
        $"New publisher {Data.Name} added successfully." :
        $"New publisher failed.",
        Data);
