using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.VendorGraph;

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