using FluentValidation;
using Kathanika.Application.Features.Authors.Commands;

namespace Kathanika.Application.Features.Authors;

internal class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator(IAuthorRepository authorRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(async (id, _) =>
            {
                return await authorRepository.GetByIdAsync(id) is not null;
            });

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
            .LessThan(x => DateOnly.FromDateTime(DateTime.Today))
            .When(x => x.MarkedAsDeceased);
    }
}