namespace Kathanika.Application.Features.Members.Commands;

internal sealed class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
{
    public CreateMemberCommandValidator(IMemberRepository memberRepository)
    {
        RuleFor(x => x.DateOfBirth)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of birth cann't be future date.");

        RuleFor(x => x.Email)
            .MustAsync(async (props, cancellationToken) =>
            {
                bool isDuplicate = await memberRepository.ExistsAsync(x => x.Status != MembershipStatus.Cancelled
                    && x.Email == props, cancellationToken);
                return !isDuplicate;
            })
            .WithMessage("Member email is already used once.");
    }
}
