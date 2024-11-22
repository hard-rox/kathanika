using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.VendorGraph;

public sealed record UpdateVendorPayload(Domain.Primitives.Result<Domain.Aggregates.VendorAggregate.Vendor> Result)
    : Payload<Domain.Aggregates.VendorAggregate.Vendor>(Result, $"Vendor {Result.Value.Name} updated successfully.");