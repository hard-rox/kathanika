
namespace Kathanika.Core.Application.Features.Members.Queries;

internal sealed class GetMembersQueryHandler(IMemberRepository memberRepository) : IRequestHandler<GetMembersQuery, IQueryable<Member>>
{
    public async Task<IQueryable<Member>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Member> membersQuery = await Task.Run(() => memberRepository.AsQueryable());
        return membersQuery;
    }
}
