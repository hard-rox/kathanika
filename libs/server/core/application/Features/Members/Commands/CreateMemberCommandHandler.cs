namespace Kathanika.Core.Application.Features.Members.Commands;

internal sealed class CreateMemberCommandHandler(IMemberRepository memberRepository) : IRequestHandler<CreateMemberCommand, Member>
{
    public async Task<Member> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        Member newMember = Member.Create(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Address,
            request.ContactNumber,
            request.Email
        );

        Member savedMember = await memberRepository.AddAsync(newMember, cancellationToken);
        return savedMember;
    }
}
