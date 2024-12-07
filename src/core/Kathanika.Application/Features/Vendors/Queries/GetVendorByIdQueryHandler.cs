using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Vendors.Queries;

internal sealed class GetVendorByIdQueryHandler(IVendorRepository vendorRepository)
    : IRequestHandler<GetVendorByIdQuery, KnResult<Vendor>>
{
    public async Task<KnResult<Vendor>> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
    {
        Vendor? vendor = await vendorRepository.GetByIdAsync(request.Id, cancellationToken);

        return vendor is null
            ? KnResult.Failure<Vendor>(VendorAggregateErrors.NotFound(request.Id))
            : KnResult.Success(vendor);
    }
}