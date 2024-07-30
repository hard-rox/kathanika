namespace Kathanika.Core.Application.Features.Members.Commands;

internal sealed class CreateMemberCommandHandler(IMemberRepository memberRepository) : IRequestHandler<CreateMemberCommand, Result<Member>>
{
    public async Task<Result<Member>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        Member newMember = Member.Create(
            request.FirstName,
            request.LastName,
            request.PhotoFileId,
            request.DateOfBirth,
            request.Address,
            request.ContactNumber,
            request.Email
        );

        Member savedMember = await memberRepository.AddAsync(newMember, cancellationToken);
        return Result.Success(savedMember);
    }
}
