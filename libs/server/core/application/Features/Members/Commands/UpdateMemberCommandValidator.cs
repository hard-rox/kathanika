namespace Kathanika.Core.Application.Features.Members.Commands;

internal sealed class UpdateMemberCommandValidator : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator(IMemberRepository memberRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(memberRepository.ExistsAsync)
            .WithMessage("Invalid member");

        RuleFor(x => x.Patch).SetValidator(new MemberPatchValidator(memberRepository));
    }
}

internal sealed class MemberPatchValidator : AbstractValidator<MemberPatch>
{
    public MemberPatchValidator(IMemberRepository memberRepository)
    {
        RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth cann't be future date.")
            .When(x => x.DateOfBirth is not null);

        RuleFor(x => x.Email)
            .MustAsync(async (props, cancellationToken) =>
            {
                bool isDuplicate = await memberRepository.ExistsAsync(x => x.Status != MembershipStatus.Cancelled
                    && x.Email == props, cancellationToken);
                return !isDuplicate;
            })
            .WithMessage("Member email is already used once.")
            .When(x => x.Email is not null);
    }
}
