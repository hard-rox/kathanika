using Microsoft.AspNetCore.Mvc;

namespace Kathanika.GraphQL.Schema;

public sealed partial class Queries
{
    [UseOffsetPaging]
    [UseFiltering]
    [UseSorting]
    public async Task<IEnumerable<Member>> GetMembersAsync(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Member> members = await mediator.Send(new GetMembersQuery(), cancellationToken);
        return members;
    }

    public async Task<Member?> GetMemberAsync([FromServices] IMediator mediator, string id, CancellationToken cancellationToken)
    {
        Member? member = await mediator.Send(new GetMemberByIdQuery(id), cancellationToken);
        return member;
    }
}
