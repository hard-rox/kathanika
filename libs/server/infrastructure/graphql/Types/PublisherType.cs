namespace Kathanika.Infrastructure.Graphql.Types;

public sealed class PublisherType : ObjectType<Publisher>
{
    protected override void Configure(IObjectTypeDescriptor<Publisher> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Name);
        descriptor.Field(x => x.Description);
        descriptor.Field(x => x.ContactInformation);
    }
}
