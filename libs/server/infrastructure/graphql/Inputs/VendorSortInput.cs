using HotChocolate.Data.Sorting;
using Kathanika.Core.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class VendorSortInput : SortInputType<Vendor>
{
    protected override void Configure(ISortInputTypeDescriptor<Vendor> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromSortInputType();
    }
}
