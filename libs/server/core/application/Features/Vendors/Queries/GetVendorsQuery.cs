using Kathanika.Core.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Core.Application.Features.Vendors.Queries;

public sealed record GetVendorsQuery : IRequest<IQueryable<Vendor>>;
