using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Vendors.Queries;

public sealed record GetVendorByIdQuery(string Id) : IRequest<Result<Vendor>>;
