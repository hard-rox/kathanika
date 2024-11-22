using HotChocolate.Resolvers;
using Kathanika.Application.Features.Vendors.Queries;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Kathanika.Infrastructure.Graphql.Schema.VendorGraph;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class VendorQueries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Domain.Aggregates.VendorAggregate.Vendor>> GetVendorsAsync(
            [Service] IMediator mediator,
            CancellationToken cancellationToken
        )
    {
        IQueryable<Domain.Aggregates.VendorAggregate.Vendor> vendors = await mediator.Send(new GetVendorsQuery(), cancellationToken);
        return vendors;
    }

    public async Task<Domain.Aggregates.VendorAggregate.Vendor?> GetVendorAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Domain.Primitives.Result<Domain.Aggregates.VendorAggregate.Vendor> result = await mediator.Send(new GetVendorByIdQuery(id), cancellationToken);
        return result.Match(context);
    }
}