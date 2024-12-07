using HotChocolate.Resolvers;
using Kathanika.Application.Features.BibRecords.Queries;
using Kathanika.Domain.Aggregates.BibRecordAggregate;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Kathanika.Infrastructure.Graphql.Schema.BibRecordGraph;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class Queries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<BibRecord>> GetBibRecordsAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        IQueryable<BibRecord> bibRecords
            = await mediator.Send(new GetBibRecordsQuery(), cancellationToken);
        return bibRecords;
    }

    public async Task<BibRecord?> GetBibRecordAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Domain.Primitives.KnResult<BibRecord> knResult
            = await mediator.Send(new GetBibRecordByIdQuery(id), cancellationToken);
        return knResult.Match(context);
    }
}