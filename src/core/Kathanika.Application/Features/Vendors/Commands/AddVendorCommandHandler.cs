using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Vendors.Commands;

internal sealed class AddVendorCommandHandler(
    ILogger<AddVendorCommandHandler> logger,
    IVendorRepository vendorRepository
) : IRequestHandler<AddVendorCommand, Result<Vendor>>
{
    public async Task<Result<Vendor>> Handle(AddVendorCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding new vendor with @{VendorName}", request.Name);

        Result<Vendor> vendorCreateResult = Vendor.Create(
            request.Name,
            request.Address,
            request.ContactNumber,
            request.Email,
            request.Website,
            request.AccountDetail,
            request.ContactPersonName,
            request.ContactPersonPhone,
            request.ContactPersonEmail
        );

        if (vendorCreateResult.IsFailure) return vendorCreateResult;

        Vendor vendor = await vendorRepository.AddAsync(vendorCreateResult.Value, cancellationToken);

        return Result.Success(vendor);
    }
}