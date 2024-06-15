namespace Kathanika.Core.Application.Features.Members.Commands;

internal sealed class RenewMembershipCommandValidator : AbstractValidator<RenewMembershipCommand>
{
    public RenewMembershipCommandValidator(IMemberRepository memberRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(async (prop, cancellationToken) =>
            {
                bool exists = await memberRepository.ExistsAsync(x => x.Status == MembershipStatus.Suspended
                    && x.Id == prop, cancellationToken);
                return exists;
            })
            .WithMessage("This member is already active or cancelled membership.");
    }
}
