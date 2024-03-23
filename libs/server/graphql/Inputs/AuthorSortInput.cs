using HotChocolate.Data.Sorting;
using Kathanika.GraphQL.GraphqlHelpers;

namespace Kathanika.GraphQL.Inputs;

public sealed class AuthorSortInput : SortInputType<Author>
{
    protected override void Configure(ISortInputTypeDescriptor<Author> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromSortInputType();
        descriptor.Ignore(x => x.Biography);
        descriptor.Ignore(x => x.FullName);
    }
}
