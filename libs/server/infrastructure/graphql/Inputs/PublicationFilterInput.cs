using HotChocolate.Data.Filters;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class PublicationFilterInput : FilterInputType<Publication>
{
    protected override void Configure(IFilterInputTypeDescriptor<Publication> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromFilterInputType();
        descriptor.Ignore(x => x.PurchaseRecords);
        descriptor.Ignore(x => x.CoverImageFileId);
    }
}
