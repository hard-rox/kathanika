namespace Kathanika.Core.Application.Features.Members.Queries;

public sealed record GetMembersQuery : IRequest<IQueryable<Member>>
{
    public GetMembersQuery()
    {
    }
}
