using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Members.Commands;

internal sealed class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, Member>
{
    private readonly IMemberRepository memberRepository;

    public UpdateMemberCommandHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task<Member> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        Member member = await memberRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundWithTheIdException(typeof(Member), request.Id);

        member.Update(
            request.Patch.FirstName,
            request.Patch.LastName,
            request.Patch.DateOfBirth,
            request.Patch.Address,
            request.Patch.ContactNumber,
            request.Patch.Email
        );

        await memberRepository.UpdateAsync(member, cancellationToken);
        return member;
    }
}
