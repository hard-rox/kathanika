using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Infrastructure.Graphql.Types;

public sealed class BibRecordType : ObjectType<BibRecord>
{
    protected override void Configure(IObjectTypeDescriptor<BibRecord> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Field(x => x.CoverImageId)
            .Name("coverImageUrl")
            .Type<UrlType>()
            .Resolve(context =>
                FileEndpointResolver.ResolveAsFileUrl(context, context.Parent<BibRecord>().CoverImageId));
    }
}