using System.Linq.Expressions;

namespace Kathanika.Core.Application.Features.Authors.Commands;

internal sealed class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
{
    public AddAuthorCommandValidator(IAuthorRepository authorRepository)
    {
        RuleFor(x => x.DateOfDeath)
            .NotNull()
            .When(x => x.MarkedAsDeceased)
            .LessThanOrEqualTo(x => DateOnly.FromDateTime(DateTime.Today))
            .GreaterThan(x => x.DateOfBirth);

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
