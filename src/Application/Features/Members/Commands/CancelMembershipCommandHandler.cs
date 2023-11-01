
using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Members.Commands;

internal sealed class CancelMembershipCommandHandler : IRequestHandler<CancelMembershipCommand>
{
    private readonly IMemberRepository memberRepository;

    public CancelMembershipCommandHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task Handle(CancelMembershipCommand request, CancellationToken cancellationToken)
    {
        Member member = await memberRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundWithTheIdException(typeof(Member), request.Id);

        member.CancelMembership();

        await memberRepository.UpdateAsync(member, cancellationToken);
    }
}
