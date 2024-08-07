using HotChocolate.Data.Sorting;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class PublisherSortInput : SortInputType<Publisher>
{
    protected override void Configure(ISortInputTypeDescriptor<Publisher> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromSortInputType();
    }
}
