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

        RuleFor(x => x.Patch).SetValidator(new MemberPatchValidator());
    }
}

internal sealed class MemberPatchValidator : AbstractValidator<MemberPatch>
{
    public MemberPatchValidator()
    {
        RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth cann't be future date.")
            .When(x => x.DateOfBirth is not null);
    }
}
