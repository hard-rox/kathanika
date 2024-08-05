namespace Kathanika.Core.Application.Features.Members.Commands;

internal sealed class CancelMembershipCommandHandler(IMemberRepository memberRepository) : IRequestHandler<CancelMembershipCommand, Result<Member>>
{
    public async Task<Result<Member>> Handle(CancelMembershipCommand request, CancellationToken cancellationToken)
    {
        Member? member = await memberRepository.GetByIdAsync(request.Id, cancellationToken);
        if (member is null)
            return Result.Failure<Member>(MemberAggregateErrors.NotFound(request.Id));

        Result cancelMembershipResult = member.CancelMembership();
        if (cancelMembershipResult.IsFailure)
            return Result.Failure<Member>(cancelMembershipResult.Errors!);

        await memberRepository.UpdateAsync(member, cancellationToken);
        return Result.Success(member);
    }
}
