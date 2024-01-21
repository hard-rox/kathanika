using HotChocolate.Data.Filters;
using Kathanika.GraphQL.GraphqlHelpers;

namespace Kathanika.GraphQL.Inputs;

public sealed class PublicationFilterInput : FilterInputType<Publication>
{
    protected override void Configure(IFilterInputTypeDescriptor<Publication> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromFilterInputType();
        descriptor.Ignore(x => x.PurchaseRecords);
    }
}
