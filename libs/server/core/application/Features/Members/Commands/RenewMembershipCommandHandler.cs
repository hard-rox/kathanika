namespace Kathanika.Core.Application.Features.Members.Commands;
internal sealed class RenewMembershipCommandHandler(IMemberRepository memberRepository) : IRequestHandler<RenewMembershipCommand, Result<Member>>
{
    public async Task<Result<Member>> Handle(RenewMembershipCommand request, CancellationToken cancellationToken)
    {
        Member? member = await memberRepository.GetByIdAsync(request.Id, cancellationToken);
        if (member is null)
            return Result.Failure<Member>(MemberAggregateErrors.NotFound(request.Id));

        Result renewMembershipResult = member.RenewMembership();
        if (renewMembershipResult.IsFailure)
            return Result.Failure<Member>(renewMembershipResult.Errors);

        await memberRepository.UpdateAsync(member, cancellationToken);
        return Result.Success(member);
    }
}
