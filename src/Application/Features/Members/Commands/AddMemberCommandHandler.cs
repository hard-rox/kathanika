namespace Kathanika.Application.Features.Members.Commands;

internal sealed class AddMemberCommandHandler : IRequestHandler<AddMemberCommand, Member>
{
    private readonly IMemberRepository memberRepository;

    public AddMemberCommandHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

    public async Task<Member> Handle(AddMemberCommand request, CancellationToken cancellationToken)
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
