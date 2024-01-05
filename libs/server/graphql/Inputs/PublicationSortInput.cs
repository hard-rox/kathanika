using HotChocolate.Data.Sorting;
using Kathanika.GraphQL.GraphqlHelpers;

namespace Kathanika.GraphQL.Inputs;

public sealed class PublicationSortInput : SortInputType<Publication>
{
    protected override void Configure(ISortInputTypeDescriptor<Publication> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromSortInputType();
    }
}
