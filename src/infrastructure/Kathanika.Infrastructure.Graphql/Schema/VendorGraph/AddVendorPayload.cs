using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.VendorGraph;

public sealed record AddVendorPayload
    : Payload<Vendor>
{
    public AddVendorPayload(KnResult<Vendor> knResult) : base(
        knResult,
        knResult.IsSuccess ? $"New vendor {knResult.Value.Name} added successfully." : "New vendor add failed."
    )
    {
    }
}