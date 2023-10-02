using System.Linq.Expressions;
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
            .WithMessage("Invalid Author");
        RuleFor(x => new {x.Id, x.Patch})
            .MustAsync(async (props, CancellationToken) =>
            {
                Expression<Func<Author, bool>> expression = a =>
                    a.Id != props.Id
                    && a.FirstName == props.Patch.FirstName
                    && a.LastName == props.Patch.LastName
                    && a.DateOfBirth == props.Patch.DateOfBirth
                    && a.Nationality == props.Patch.Nationality;
                bool isDuplicate = await authorRepository.ExistsAsync(expression, CancellationToken);
                return !isDuplicate;
            })
            .WithName("FirstName, LastName, DateOfBirth, Nationality")
            .WithMessage("Duplicate author information {PropertyName}");

        RuleFor(x => x.Patch)
            .SetValidator(new AuthorPatchValidator(authorRepository));
    }
}

internal class AuthorPatchValidator : AbstractValidator<UpdateAuthorCommand.AuthorPatch>
{
    public AuthorPatchValidator(IAuthorRepository authorRepository)
    {
        RuleFor(x => x.DateOfDeath)
            .NotNull()
            .When(x => x.MarkedAsDeceased)
            .LessThanOrEqualTo(x => DateOnly.FromDateTime(DateTime.Today))
            .GreaterThan(x => x.DateOfBirth)
            .When(x => x.DateOfBirth is not null);
    }
}
