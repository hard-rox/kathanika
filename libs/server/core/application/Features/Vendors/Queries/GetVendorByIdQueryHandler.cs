using Kathanika.Core.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Core.Application.Features.Vendors.Queries;

internal sealed class GetVendorByIdQueryHandler(IVendorRepository vendorRepository)
    : IRequestHandler<GetVendorByIdQuery, Result<Vendor>>
{
    public async Task<Result<Vendor>> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
    {
        Vendor? vendor = await vendorRepository.GetByIdAsync(request.Id, cancellationToken);

        return vendor is null
            ? Result.Failure<Vendor>(VendorAggregateErrors.NotFound(request.Id))
            : Result.Success(vendor);
    }
}
