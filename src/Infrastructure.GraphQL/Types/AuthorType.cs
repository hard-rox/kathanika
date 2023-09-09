namespace Kathanika.Infrastructure.GraphQL.Types;

public sealed class AuthorType : ObjectType<Author>
{
    protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.FirstName);
        descriptor.Field(x => x.LastName);
        descriptor.Field(x => x.FullName);
        descriptor.Field(x => x.DateOfBirth);
        descriptor.Field(x => x.DateOfDeath);
        descriptor.Field(x => x.Nationality);
        descriptor.Field(x => x.Biography);
    }
}