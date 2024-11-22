using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Vendors.Commands;

internal sealed class DeleteVendorCommandHandler(
    IVendorRepository vendorRepository)
    : IRequestHandler<DeleteVendorCommand, Result>
{
    public async Task<Result> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
    {
        if (await vendorRepository.GetByIdAsync(request.Id, cancellationToken) is null)
            return VendorAggregateErrors.NotFound(request.Id);

        await vendorRepository.DeleteAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}