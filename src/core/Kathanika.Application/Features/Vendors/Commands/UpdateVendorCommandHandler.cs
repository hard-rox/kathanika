using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.Vendors.Commands;

internal sealed class UpdateVendorCommandHandler(IVendorRepository vendorRepository)
    : ICommandHandler<UpdateVendorCommand, KnResult<Vendor>>
{
    public async Task<KnResult<Vendor>> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        Vendor? existingVendor = await vendorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existingVendor is null)
            return VendorAggregateErrors.NotFound(request.Id);

        existingVendor.Update(
            request.Patch.Name,
            request.Patch.Address,
            request.Patch.ContactNumber,
            request.Patch.Email,
            request.Patch.Website,
            request.Patch.AccountDetail,
            request.Patch.ContactPersonName,
            request.Patch.ContactPersonPhone,
            request.Patch.ContactPersonEmail
        );

        await vendorRepository.UpdateAsync(existingVendor, cancellationToken);
        return existingVendor;
    }
}