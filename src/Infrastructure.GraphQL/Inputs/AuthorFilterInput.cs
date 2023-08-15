using HotChocolate.Data.Filters;
using HotChocolate.Types.Descriptors;

namespace Kathanika.Infrastructure.GraphQL.Inputs;

public sealed class AuthorFilterInput : FilterInputType<Author>
{
    protected override void Configure(IFilterInputTypeDescriptor<Author> descriptor)
    {
        descriptor.Ignore(x => x.Biography);
        descriptor.Ignore(x => x.FullName);
    }
}