using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Vendors.Commands;

internal sealed class UpdateVendorCommandHandler(IVendorRepository vendorRepository)
    : IRequestHandler<UpdateVendorCommand, Result<Vendor>>
{
    public async Task<Result<Vendor>> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        Vendor? existingVendor = await vendorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existingVendor is null)
            return Result.Failure<Vendor>(VendorAggregateErrors.NotFound(request.Id));

        existingVendor.Update(
            request.Patch.Name,
            request.Patch.Address,
            request.Patch.ContactNumber,
            request.Patch.Email,
            request.Patch.Website,
            request.Patch.AccountDetail,
            request.Patch.ContactPersonName,
            request.Patch.ContactPersonPhone,
            request.Patch.ContactPersonEmail,
            request.Patch.Status
        );

        await vendorRepository.UpdateAsync(existingVendor, cancellationToken);
        return Result.Success(existingVendor);
    }
}