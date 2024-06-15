using HotChocolate.Data.Filters;
using Kathanika.Infrastructure.Graphql.GraphqlHelpers;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class PublicationFilterInput : FilterInputType<Publication>
{
    protected override void Configure(IFilterInputTypeDescriptor<Publication> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromFilterInputType();
        descriptor.Ignore(x => x.PurchaseRecords);
    }
}
