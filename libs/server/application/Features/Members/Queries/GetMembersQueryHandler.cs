
namespace Kathanika.Application.Features.Members.Queries;

internal sealed class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IQueryable<Member>>
{
    private readonly IMemberRepository memberRepository;

    public GetMembersQueryHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task<IQueryable<Member>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Member> membersQuery = await Task.Run(() => memberRepository.AsQueryable());
        return membersQuery;
    }
}
