namespace Kathanika.Application.Features.Members.Commands;

internal sealed class CancelMembershipCommandValidator : AbstractValidator<CancelMembershipCommand>
{
    public CancelMembershipCommandValidator(IMemberRepository memberRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(async (prop, cancellationToken) =>
            {
                bool exists = await memberRepository.ExistsAsync(x => x.Status != MembershipStatus.Cancelled
                    && x.Id == prop, cancellationToken);
                return exists;
            })
            .WithMessage("Member not found or this member already cancelled membership.");
    }
}
