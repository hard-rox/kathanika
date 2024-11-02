using HotChocolate.Resolvers;
using Kathanika.Core.Application.Features.BibRecords.Queries;
using Kathanika.Core.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed partial class Queries
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
        Core.Domain.Primitives.Result<BibRecord> result
            = await mediator.Send(new GetBibRecordByIdQuery(id), cancellationToken);
        return result.Match(context);
    }
}
