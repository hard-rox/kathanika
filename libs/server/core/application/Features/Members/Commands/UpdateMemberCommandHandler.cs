namespace Kathanika.Core.Application.Features.Members.Commands;

internal sealed class UpdateMemberCommandHandler(IMemberRepository memberRepository) : IRequestHandler<UpdateMemberCommand, Result<Member>>
{
    public async Task<Result<Member>> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        Member? member = await memberRepository.GetByIdAsync(request.Id, cancellationToken);
        if (member is null)
            return Result.Failure<Member>(MemberAggregateErrors.NotFound(request.Id));

        Result updateMemberResult = member.Update(
            request.Patch.FirstName,
            request.Patch.LastName,
            request.Patch.PhotoFileId,
            request.Patch.DateOfBirth,
            request.Patch.Address,
            request.Patch.ContactNumber,
            request.Patch.Email
        );
        if (updateMemberResult.IsFailure)
            return Result.Failure<Member>(updateMemberResult.Errors);

        await memberRepository.UpdateAsync(member, cancellationToken);
        return Result.Success(member);
    }
}
