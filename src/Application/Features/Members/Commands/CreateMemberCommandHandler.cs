namespace Kathanika.Application.Features.Members.Commands;

internal sealed class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Member>
{
    private readonly IMemberRepository memberRepository;

    public CreateMemberCommandHandler(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }

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
