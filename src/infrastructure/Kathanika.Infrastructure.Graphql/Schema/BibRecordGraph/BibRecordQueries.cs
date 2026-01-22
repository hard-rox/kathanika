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
        [Service] IDispatcher dispatcher,
        CancellationToken cancellationToken
    )
    {
        IQueryable<BibRecord> bibRecords
            = await dispatcher.Send(new GetBibRecordsQuery(), cancellationToken);
        return bibRecords;
    }

    public async Task<BibRecord?> GetBibRecordAsync(
        [Service] IDispatcher dispatcher,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        KnResult<BibRecord> knResult
            = await dispatcher.Send(new GetBibRecordByIdQuery(id), cancellationToken);
        return knResult.Match(context);
    }
}