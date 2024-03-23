using HotChocolate.Data.Filters;
using Kathanika.GraphQL.GraphqlHelpers;

namespace Kathanika.GraphQL.Inputs;

public sealed class PublisherFilterInput : FilterInputType<Publisher>
{
    protected override void Configure(IFilterInputTypeDescriptor<Publisher> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromFilterInputType();
    }
}
