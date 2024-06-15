using HotChocolate.Data.Filters;
using Kathanika.Infrastructure.Graphql.GraphqlHelpers;

namespace Kathanika.Infrastructure.Graphql.Inputs;

public sealed class MemberFilterInput : FilterInputType<Member>
{
    protected override void Configure(IFilterInputTypeDescriptor<Member> descriptor)
    {
        descriptor.IgnoreAuditFieldsFromFilterInputType();
        descriptor.Ignore(x => x.FullName);
    }
}
