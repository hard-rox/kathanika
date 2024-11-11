using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record UpdateVendorPayload(Domain.Primitives.Result<Vendor> Result)
    : Payload<Vendor>(Result, $"Vendor {Result.Value.Name} updated successfully.");