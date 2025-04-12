using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.Vendors.Commands;

internal sealed class DeleteVendorCommandHandler(
    IVendorRepository vendorRepository)
    : IRequestHandler<DeleteVendorCommand, KnResult>
{
    public async Task<KnResult> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {
        if (await vendorRepository.GetByIdAsync(request.Id, cancellationToken) is null)
            return VendorAggregateErrors.NotFound(request.Id);

        await vendorRepository.DeleteAsync(request.Id, cancellationToken);

        return KnResult.Success();
    }
}