using HotChocolate.Data.Sorting;
using Kathanika.GraphQL.GraphqlHelpers;

namespace Kathanika.GraphQL.Inputs;

public sealed class MemberSortInput : SortInputType<Member>
{
    protected override void Configure(ISortInputTypeDescriptor<Member> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromSortInputType();
        descriptor.Ignore(x => x.FullName);
    }
}
