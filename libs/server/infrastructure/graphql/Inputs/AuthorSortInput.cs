using HotChocolate.Data.Sorting;
using Kathanika.Infrastructure.Graphql.GraphqlHelpers;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class AuthorSortInput : SortInputType<Author>
{
    protected override void Configure(ISortInputTypeDescriptor<Author> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromSortInputType();
        descriptor.Ignore(x => x.Biography);
        descriptor.Ignore(x => x.FullName);
    }
}
