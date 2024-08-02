using HotChocolate.Data.Sorting;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class MemberSortInput : SortInputType<Member>
{
    protected override void Configure(ISortInputTypeDescriptor<Member> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromSortInputType();
        descriptor.Ignore(x => x.FullName);
        descriptor.Ignore(x => x.PhotoFileId);
    }
}
