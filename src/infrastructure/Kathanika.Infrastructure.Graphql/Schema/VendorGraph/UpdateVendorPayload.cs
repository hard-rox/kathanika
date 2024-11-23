using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.VendorGraph;

public sealed record UpdateVendorPayload
    : Payload<Vendor>
{
    public UpdateVendorPayload(Domain.Primitives.Result<Vendor> result)
        : base(result,
            result.IsSuccess
                ? $"New vendor {result.Value.Name} added successfully."
                : "New vendor addition failed.")
    {
    }
}