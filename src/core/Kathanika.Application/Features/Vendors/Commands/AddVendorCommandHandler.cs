using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.Vendors.Commands;

internal sealed class AddVendorCommandHandler(
    ILogger<AddVendorCommandHandler> logger,
    IVendorRepository vendorRepository
) : IRequestHandler<AddVendorCommand, KnResult<Vendor>>
{
    public async Task<KnResult<Vendor>> Handle(AddVendorCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding new vendor with @{VendorName}", request.Name);

        KnResult<Vendor> vendorCreateResult = Vendor.Create(
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

        return KnResult.Success(vendor);
    }
}