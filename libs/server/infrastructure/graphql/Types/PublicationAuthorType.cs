using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Types;

public sealed class PublicationAuthorType : AbstractValueObjectType<PublicationAuthor>
{
    protected override void Configure(IObjectTypeDescriptor<PublicationAuthor> descriptor)
    {
        descriptor.Field(x => x.DpFileId)
            .Name("dp")
            .Type<UrlType>()
            .Resolve(context => FileEndpointResolver.ResolveAsFileUrl(context, context.Parent<PublicationAuthor>().DpFileId));
        base.Configure(descriptor);
    }
}
