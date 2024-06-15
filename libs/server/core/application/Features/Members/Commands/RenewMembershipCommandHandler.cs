using Kathanika.Core.Domain.Exceptions;

namespace Kathanika.Core.Application.Features.Members.Commands;
internal sealed class RenewMembershipCommandHandler(IMemberRepository memberRepository) : IRequestHandler<RenewMembershipCommand, Member>
{
    public async Task<Member> Handle(RenewMembershipCommand request, CancellationToken cancellationToken)
    {
        Member member = await memberRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundWithTheIdException(typeof(Member), request.Id);

        member.RenewMembership();

        await memberRepository.UpdateAsync(member, cancellationToken);
        return member;
    }

}
