using HotChocolate.Resolvers;
using Kathanika.Application.Features.Vendors.Queries;
using Kathanika.Domain.Aggregates.VendorAggregate;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Kathanika.Infrastructure.Graphql.Schema.VendorGraph;

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
        Domain.Primitives.Result<Vendor> result = await mediator.Send(new GetVendorByIdQuery(id), cancellationToken);
        return result.Match(context);
    }
}