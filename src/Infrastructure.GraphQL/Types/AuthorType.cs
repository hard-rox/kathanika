namespace Kathanika.Infrastructure.GraphQL.Types;

public sealed class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.Field(x => x.Id).Type<NonNullType<StringType>>();
        descriptor.Field(x => x.DateOfBirth).Type<NonNullType<DateType>>();
        descriptor.Field(x => x.DateOfDeath).Type<DateType>();
    }
}
