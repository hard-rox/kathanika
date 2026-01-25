using HotChocolate.Resolvers;
using Kathanika.Application.Features.BibItems.Queries;
using Kathanika.Domain.Aggregates.BibItemAggregate;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace Kathanika.Infrastructure.Graphql.Schema.BibItemGraph;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class BibItemQueries
{
    public async Task<BibItem?> GetBibItemAsync(
        [Service] IDispatcher dispatcher,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        KnResult<BibItem> knResult = await dispatcher.Send(new GetBibItemByIdQuery(id), cancellationToken);
        return knResult.Match(context);
    }
}