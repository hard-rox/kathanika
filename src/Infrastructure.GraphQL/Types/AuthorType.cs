namespace Kathanika.Infrastructure.GraphQL.Types;

public sealed class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        
        descriptor.Field(x => x.Id).Type<NonNullType<StringType>>();
        descriptor.Field(x => x.FirstName);
        descriptor.Field(x => x.LastName);
        descriptor.Field(x => x.DateOfBirth).Type<NonNullType<DateType>>();
        descriptor.Field(x => x.DateOfDeath).Type<DateType>();
        descriptor.Field(x => x.Nationality);
        descriptor.Field(x => x.Biography);
    }
}
