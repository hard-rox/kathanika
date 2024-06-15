namespace Kathanika.Core.Application.Features.Members.Commands;

internal sealed class SuspendMembershipCommandValidator : AbstractValidator<SuspendMembershipCommand>
{
    public SuspendMembershipCommandValidator(IMemberRepository memberRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(async (prop, cancellationToken) =>
            {
                bool exists = await memberRepository.ExistsAsync(x => x.Status == MembershipStatus.Active
                    && x.Id == prop, cancellationToken);
                return exists;
            })
            .WithMessage("This member is already suspended or cancelled membership.");
    }
}
