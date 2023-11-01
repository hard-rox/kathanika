
using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Members.Commands;

internal sealed class SuspendMembershipCommandHandler : IRequestHandler<SuspendMembershipCommand>
{
    private readonly IMemberRepository memberRepository;

    public SuspendMembershipCommandHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task Handle(SuspendMembershipCommand request, CancellationToken cancellationToken)
    {
        Member member = await memberRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundWithTheIdException(typeof(Member), request.Id);

        member.SuspendMembership();

        await memberRepository.UpdateAsync(member, cancellationToken);
    }
}
