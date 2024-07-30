using HotChocolate.Data.Filters;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class PublisherFilterInput : FilterInputType<Publisher>
{
    protected override void Configure(IFilterInputTypeDescriptor<Publisher> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromFilterInputType();
    }
}
