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
        
        RuleFor(x => new { x.FirstName, x.LastName, x.DateOfBirth, x.Nationality })
            .MustAsync(async (props, CancellationToken) =>
            {
                Expression<Func<Author, bool>> expression = a =>
                    a.FirstName == props.FirstName
                    && a.LastName == props.LastName
                    && a.DateOfBirth == props.DateOfBirth
                    && a.Nationality == props.Nationality;
                bool isDuplicate = await authorRepository.ExistsAsync(expression, CancellationToken);
                return !isDuplicate;
            })
            .WithName("FirstName, LastName, DateOfBirth, Nationality")
            .WithMessage("Duplicate author information {PropertyName}");
    }
}
