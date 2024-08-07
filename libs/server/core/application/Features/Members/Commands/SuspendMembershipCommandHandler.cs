namespace Kathanika.Core.Application.Features.Members.Commands;

internal sealed class SuspendMembershipCommandHandler(IMemberRepository memberRepository) : IRequestHandler<SuspendMembershipCommand, Result<Member>>
{
    public async Task<Result<Member>> Handle(SuspendMembershipCommand request, CancellationToken cancellationToken)
    {
        Member? member = await memberRepository.GetByIdAsync(request.Id, cancellationToken);
        if (member is null)
            return Result.Failure<Member>(MemberAggregateErrors.NotFound(request.Id));

        Result suspendMembershipResult = member.SuspendMembership();
        if (suspendMembershipResult.IsFailure)
            return Result.Failure<Member>(suspendMembershipResult.Errors);

        await memberRepository.UpdateAsync(member, cancellationToken);
        return Result.Success(member);
    }
}
