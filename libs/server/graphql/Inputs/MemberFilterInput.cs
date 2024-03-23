using HotChocolate.Data.Filters;
using Kathanika.GraphQL.GraphqlHelpers;

namespace Kathanika.GraphQL.Inputs;

public sealed class MemberFilterInput : FilterInputType<Member>
{
    protected override void Configure(IFilterInputTypeDescriptor<Member> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromFilterInputType();
        descriptor.Ignore(x => x.FullName);
    }
}
