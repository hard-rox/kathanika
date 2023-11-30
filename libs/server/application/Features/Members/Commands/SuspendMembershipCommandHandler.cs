
using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Members.Commands;

internal sealed class SuspendMembershipCommandHandler(IMemberRepository memberRepository) : IRequestHandler<SuspendMembershipCommand, Member>
{
    public async Task<Member> Handle(SuspendMembershipCommand request, CancellationToken cancellationToken)
    {
        Member member = await memberRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundWithTheIdException(typeof(Member), request.Id);

        member.SuspendMembership();

        await memberRepository.UpdateAsync(member, cancellationToken);
        return member;
    }
}
