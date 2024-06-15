using HotChocolate.Data.Filters;
using Kathanika.Infrastructure.Graphql.GraphqlHelpers;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class AuthorFilterInput : FilterInputType<Author>
{
    protected override void Configure(IFilterInputTypeDescriptor<Author> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromFilterInputType();
        descriptor.Ignore(x => x.Biography);
        descriptor.Ignore(x => x.FullName);
    }
}
