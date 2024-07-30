using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record UpdatePublisherPayload(Publisher? Data)
    : Payload<Publisher>(
        Data is not null ?
        $"Publisher {Data.Name} updated successfully." :
        $"Publisher update failed.",
        Data);
