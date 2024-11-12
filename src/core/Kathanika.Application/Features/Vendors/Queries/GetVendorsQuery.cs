using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.Vendors.Queries;

public sealed record GetVendorsQuery : IRequest<IQueryable<Vendor>>;