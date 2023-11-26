namespace Kathanika.GraphQL.Types;

public sealed class PublicationAuthorType : ObjectType<PublicationAuthor>
{
    protected override void Configure(IObjectTypeDescriptor<PublicationAuthor> descriptor)
    {
        descriptor.Ignore(x => x.GetAtomicValues());
    }
}
