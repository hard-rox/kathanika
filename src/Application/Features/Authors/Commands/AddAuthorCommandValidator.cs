namespace Kathanika.Application.Features.Authors.Commands;

internal sealed class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
{
    public AddAuthorCommandValidator()
    {
        RuleFor(x => x.DateOfDeath)
            .NotNull()
            .When(x => x.MarkedAsDeceased)
            .LessThanOrEqualTo(x => DateOnly.FromDateTime(DateTime.Today))
            .GreaterThan(x => x.DateOfBirth);
    }
}
