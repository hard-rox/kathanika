using HotChocolate.Data.Sorting;
using Kathanika.Infrastructure.Graphql.GraphqlHelpers;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class PublicationSortInput : SortInputType<Publication>
{
    protected override void Configure(ISortInputTypeDescriptor<Publication> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromSortInputType();
        descriptor.Ignore(x => x.PurchaseRecords);
    }
}
