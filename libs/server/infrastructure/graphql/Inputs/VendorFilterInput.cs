using HotChocolate.Data.Filters;
using Kathanika.Core.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class VendorFilterInput : FilterInputType<Vendor>
{
    protected override void Configure(IFilterInputTypeDescriptor<Vendor> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromFilterInputType();
    }
}
