namespace Kathanika.Core.Application.Features.Members.Commands;

internal sealed class CreateMemberCommandHandler(IMemberRepository memberRepository) : IRequestHandler<CreateMemberCommand, Result<Member>>
{
    public async Task<Result<Member>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        Result<Member> result = Member.Create(
            request.FirstName,
            request.LastName,
            request.PhotoFileId,
            request.DateOfBirth,
            request.Address,
            request.ContactNumber,
            request.Email
        );

        if (result.IsFailure)
            return Result.Failure<Member>(result.Errors!);

        Member savedMember = await memberRepository.AddAsync(result.Value, cancellationToken);
        return Result.Success(savedMember);
    }
}
