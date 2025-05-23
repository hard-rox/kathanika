using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.Vendors.Queries;

public sealed record GetVendorByIdQuery(string Id) : IRequest<KnResult<Vendor>>;