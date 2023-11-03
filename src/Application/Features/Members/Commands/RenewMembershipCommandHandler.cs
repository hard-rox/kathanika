using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Members.Commands;
internal sealed class RenewMembershipCommandHandler : IRequestHandler<RenewMembershipCommand, Member>
{
    private readonly IMemberRepository memberRepository;

    public RenewMembershipCommandHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task<Member> Handle(RenewMembershipCommand request, CancellationToken cancellationToken)
    {
        Member member = await memberRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundWithTheIdException(typeof(Member), request.Id);

        member.RenewMembership();

        await memberRepository.UpdateAsync(member, cancellationToken);
        return member;
    }

}
