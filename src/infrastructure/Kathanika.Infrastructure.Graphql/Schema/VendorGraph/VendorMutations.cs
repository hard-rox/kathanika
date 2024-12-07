using Kathanika.Application.Features.Vendors.Commands;
using Kathanika.Domain.Aggregates.VendorAggregate;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Kathanika.Infrastructure.Graphql.Schema.VendorGraph;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class VendorMutations
{
    public async Task<AddVendorPayload> AddVendorAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        AddVendorCommand input
    )
    {
        Domain.Primitives.KnResult<Vendor> knResult = await mediator.Send(input, cancellationToken);
        return new AddVendorPayload(knResult);
    }

    public async Task<UpdateVendorPayload> UpdateVendorAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        UpdateVendorCommand input
    )
    {
        Domain.Primitives.KnResult<Vendor> knResult = await mediator.Send(input, cancellationToken);
        return new UpdateVendorPayload(knResult);
    }

    public async Task<DeleteVendorPayload> DeleteVendorAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken,
        string id
    )
    {
        KnResult knResult = await mediator.Send(new DeleteVendorCommand(id), cancellationToken);
        return new DeleteVendorPayload(id, knResult);
    }
}