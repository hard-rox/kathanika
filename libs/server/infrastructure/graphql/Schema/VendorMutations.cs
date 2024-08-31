using Kathanika.Core.Application.Features.Vendors.Commands;
using Kathanika.Core.Domain.Aggregates.VendorAggregate;
using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class VendorMutations
{
    public async Task<AddVendorPayload> AddVendorAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        AddVendorCommand input
    )
    {
        Core.Domain.Primitives.Result<Vendor> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<UpdateVendorPayload> UpdateVendorAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        UpdateVendorCommand input
    )
    {
        Core.Domain.Primitives.Result<Vendor> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<DeleteVendorPayload> DeleteVendorAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        string id
    )
    {
        Result result = await mediator.Send(new DeleteVendorCommand(id), cancellationToken);
        return new(id, result);
    }
}
