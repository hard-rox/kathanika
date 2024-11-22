using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.VendorGraph;

public sealed record AddVendorPayload
    : Payload<Domain.Aggregates.VendorAggregate.Vendor>
{
    public AddVendorPayload(Domain.Primitives.Result<Domain.Aggregates.VendorAggregate.Vendor> result) : base(
        result,
        result.IsSuccess ?
        $"New vendor {result.Value.Name} added successfully." :
        "New vendor add failed."
    )
    { }
}