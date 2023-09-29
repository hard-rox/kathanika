using Kathanika.Application.Features.Authors.Commands;

namespace Kathanika.Application.Features.Authors;

internal sealed class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator(IAuthorRepository authorRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(authorRepository.ExistsAsync)
            .WithMessage("Invalid Author Id");

        RuleFor(x => x.Patch)
            .SetValidator(new AuthorPatchValidator());
    }
}

internal class AuthorPatchValidator : AbstractValidator<UpdateAuthorCommand.AuthorPatch>
{
    public AuthorPatchValidator()
    {
        RuleFor(x => x.DateOfDeath)
            .NotNull()
            .When(x => x.MarkedAsDeceased)
            .LessThanOrEqualTo(x => DateOnly.FromDateTime(DateTime.Today))
            .GreaterThan(x => x.DateOfBirth)
            .When(x => x.DateOfBirth is not null);
    }
}
