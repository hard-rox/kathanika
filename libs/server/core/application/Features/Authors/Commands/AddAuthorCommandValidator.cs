using System.Linq.Expressions;
using Kathanika.Core.Application.Services;

namespace Kathanika.Core.Application.Features.Authors.Commands;

internal sealed class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
{
    public AddAuthorCommandValidator(
        IAuthorRepository authorRepository,
        IFileStore fileStore
    )
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
        RuleFor(x => x.DpFileId)
            .MustAsync(async (props, cancellationToken) =>
                await fileStore.ValidateAsync(props ?? string.Empty, 1, 2000000, ["image/*"], [".jpeg", ".jpg", ".png"], cancellationToken))
            .WithMessage("Invalid file, Check file type, content type and size")
            .When(x => x.DpFileId is not null);
    }
}
