using Kathanika.Application.Features.BibItems.Queries;
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
        descriptor.Field(x => x.CreatedAt);
        descriptor.Field(x => x.CreatedByUserName);
        descriptor.Field(x => x.LastModifiedAt);
        descriptor.Field(x => x.LastModifiedByUserName);
        
        descriptor.Field("items")
            .Resolve(context =>
            {
                IMediator mediator = context.Service<IMediator>();
                return mediator.Send(new GetBibItemsQuery(context.Parent<BibRecord>().Id));
            });
    }
}