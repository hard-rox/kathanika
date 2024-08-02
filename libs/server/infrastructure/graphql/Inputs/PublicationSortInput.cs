using HotChocolate.Data.Sorting;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class PublicationSortInput : SortInputType<Publication>
{
    protected override void Configure(ISortInputTypeDescriptor<Publication> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromSortInputType();
        descriptor.Ignore(x => x.PurchaseRecords);
        descriptor.Ignore(x => x.CoverImageFileId);
    }
}
