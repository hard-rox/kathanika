namespace Kathanika.Infrastructure.Graphql.Types;

public sealed class PublicationType : ObjectType<Publication>
{
    protected override void Configure(IObjectTypeDescriptor<Publication> descriptor)
    {
        descriptor.BindFieldsExplicitly();

        descriptor.Field(x => x.Id);
        descriptor.Field(x => x.Title);
        descriptor.Field(x => x.Isbn);
        descriptor.Field(x => x.PublicationType);
        descriptor.Field(x => x.Publisher)
            .Type<PublicationPublisherType>();
        descriptor.Field(x => x.PublishedDate);
        descriptor.Field(x => x.Edition);
        descriptor.Field(x => x.Description);
        descriptor.Field(x => x.Language);
        descriptor.Field(x => x.CopiesAvailable);
        descriptor.Field(x => x.CallNumber);
        descriptor.Field(x => x.Authors);
        descriptor.Field(x => x.PurchaseRecords)
            .UseSorting();
        descriptor.Field(x => x.DonationRecords)
            .UseSorting();
        descriptor.Field(x => x.CoverImageFileId)
            .Name("coverImage")
            .Type<UrlType>()
            .Resolve(context => FileEndpointResolver.ResolveAsFileUrl(context, context.Parent<Publication>().CoverImageFileId));
    }
}
