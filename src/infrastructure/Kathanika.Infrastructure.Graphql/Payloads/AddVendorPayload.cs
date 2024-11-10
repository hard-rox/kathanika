using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;

public sealed record AddVendorPayload
    : Payload<Vendor>
{
    public AddVendorPayload(Domain.Primitives.Result<Vendor> result) : base(
        result,
        result.IsSuccess ?
        $"New vendor {result.Value?.Name} added successfully." :
        $"New vendor add failed."
    )
    { }
}
