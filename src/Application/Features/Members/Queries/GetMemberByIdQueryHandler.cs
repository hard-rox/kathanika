
namespace Kathanika.Application.Features.Members.Queries;

internal sealed class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, Member?>
{
    private readonly IMemberRepository memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task<Member?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        Member? member = await memberRepository.GetByIdAsync(request.Id, cancellationToken);
        return member;
    }
}
