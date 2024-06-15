using Kathanika.Core.Domain.Exceptions;

namespace Kathanika.Core.Application.Features.Members.Commands;

internal sealed class UpdateMemberCommandHandler(IMemberRepository memberRepository) : IRequestHandler<UpdateMemberCommand, Member>
{
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
