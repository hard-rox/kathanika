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
}
