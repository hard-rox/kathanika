using HotChocolate.Resolvers;
using Kathanika.Core.Application.Features.Vendors.Queries;
using Kathanika.Core.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class VendorQueries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Vendor>> GetVendorsAsync(
            [Service] IMediator mediator,
            CancellationToken cancellationToken
        )
    {
        IQueryable<Vendor> vendors = await mediator.Send(new GetVendorsQuery(), cancellationToken);
        return vendors;
    }

    public async Task<Vendor?> GetVendorAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Vendor> result = await mediator.Send(new GetVendorByIdQuery(id), cancellationToken);
        return result.Match(context);
    }
}
