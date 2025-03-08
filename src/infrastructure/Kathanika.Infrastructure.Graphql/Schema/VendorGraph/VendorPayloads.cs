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

public sealed record DeleteVendorPayload
    : Payload
{
    public DeleteVendorPayload(string id, KnResult knResult) : base(
        knResult,
        knResult.IsSuccess ? $"Vendor with Id: {id} deleted." : $"Vendor with Id: {id} deletion failed."
    )
    {
    }
}

public sealed record UpdateVendorPayload
    : Payload<Vendor>
{
    public UpdateVendorPayload(KnResult<Vendor> knResult)
        : base(knResult,
            knResult.IsSuccess
                ? $"New vendor {knResult.Value.Name} added successfully."
                : "New vendor addition failed.")
    {
    }
}