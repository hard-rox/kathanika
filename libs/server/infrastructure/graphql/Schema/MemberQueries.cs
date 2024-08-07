using HotChocolate.Resolvers;
using Kathanika.Core.Application.Features.Members.Queries;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Query)]
public sealed class MemberQueries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Member>> GetMembersAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Member> members = await mediator.Send(new GetMembersQuery(), cancellationToken);
        return members;
    }

    public async Task<Member?> GetMemberAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(new GetMemberByIdQuery(id), cancellationToken);
        return result.Match(context);
    }
}
