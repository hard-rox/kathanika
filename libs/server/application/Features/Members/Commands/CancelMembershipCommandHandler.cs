
using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Members.Commands;

internal sealed class CancelMembershipCommandHandler(IMemberRepository memberRepository) : IRequestHandler<CancelMembershipCommand, Member>
{
    public async Task<Member> Handle(CancelMembershipCommand request, CancellationToken cancellationToken)
    {
        Member member = await memberRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundWithTheIdException(typeof(Member), request.Id);

        member.CancelMembership();

        await memberRepository.UpdateAsync(member, cancellationToken);
        return member;
    }
}
