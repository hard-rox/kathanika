
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.Vendors.Queries;

internal sealed class GetVendorsQueryHandler(IVendorRepository vendorRepository) : IRequestHandler<GetVendorsQuery, IQueryable<Vendor>>
{
    public async Task<IQueryable<Vendor>> Handle(GetVendorsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Vendor> vendorsQuery = await Task.Run(vendorRepository.AsQueryable, cancellationToken);
        return vendorsQuery;
    }
}