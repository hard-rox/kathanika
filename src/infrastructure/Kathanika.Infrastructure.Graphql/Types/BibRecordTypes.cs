using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using Kathanika.Application.Features.BibItems.Queries;
using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Infrastructure.Graphql.Types;

public sealed class BibRecordType : ObjectType<BibRecord>
{
    protected override void Configure(IObjectTypeDescriptor<BibRecord> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Ignore(x => x.MarcMetadata);

        descriptor.Field(x => x.CreatedAt);
        descriptor.Field(x => x.CreatedByUserName);
        descriptor.Field(x => x.LastModifiedAt);
        descriptor.Field(x => x.LastModifiedByUserName);

        descriptor.Field(x => x.CoverImageId)
            .Name("coverImageUrl")
            .Type<UrlType>()
            .Resolve(context =>
                FileEndpointResolver.ResolveAsFileUrl(context, context.Parent<BibRecord>().CoverImageId));
        descriptor.Field("bibItems")
            .Resolve(context =>
            {
                IMediator mediator = context.Service<IMediator>();
                return mediator.Send(new GetBibItemsQuery(context.Parent<BibRecord>().Id));
            });
    }
}

public sealed class BibRecordSortInputType : SortInputType<BibRecord>
{
    protected override void Configure(ISortInputTypeDescriptor<BibRecord> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Ignore(x => x.MarcMetadata);
        descriptor.Ignore(x => x.CoverImageId);
    }
}

public sealed class BibRecordFilterInputType : FilterInputType<BibRecord>
{
    protected override void Configure(IFilterInputTypeDescriptor<BibRecord> descriptor)
    {
        base.Configure(descriptor);
        descriptor.Ignore(x => x.MarcMetadata);
        descriptor.Ignore(x => x.CoverImageId);
    }
}