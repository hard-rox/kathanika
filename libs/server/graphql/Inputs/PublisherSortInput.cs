using HotChocolate.Data.Sorting;
using Kathanika.GraphQL.GraphqlHelpers;

namespace Kathanika.GraphQL.Inputs;

public sealed class PublisherSortInput : SortInputType<Publisher>
{
    protected override void Configure(ISortInputTypeDescriptor<Publisher> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromSortInputType();
    }
}
