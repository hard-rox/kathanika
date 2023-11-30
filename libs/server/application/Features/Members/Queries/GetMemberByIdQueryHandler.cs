
namespace Kathanika.Application.Features.Members.Queries;

internal sealed class GetMemberByIdQueryHandler(IMemberRepository memberRepository) : IRequestHandler<GetMemberByIdQuery, Member?>
{
    public async Task<Member?> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        Member? member = await memberRepository.GetByIdAsync(request.Id, cancellationToken);
        return member;
    }
}
