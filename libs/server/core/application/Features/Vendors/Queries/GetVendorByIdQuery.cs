using Kathanika.Core.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Core.Application.Features.Vendors.Queries;

public sealed record GetVendorByIdQuery(string Id) : IRequest<Result<Vendor>>;
