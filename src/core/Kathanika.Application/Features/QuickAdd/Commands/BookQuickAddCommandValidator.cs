using Kathanika.Application.CommonValidators;
using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.QuickAdd.Commands;

internal sealed class BookQuickAddCommandValidator : AbstractValidator<BookQuickAddCommand>
{
    public BookQuickAddCommandValidator(IBibRecordRepository bibRecordRepository)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required and cannot be empty.");

        RuleFor(x => x.Author)
            .NotEmpty()
            .WithMessage("Author is required and cannot be empty.");

        RuleFor(x => x.NumberOfCopies)
            .GreaterThan(0)
            .WithMessage("Number of copies must be greater than 0.")
            .LessThanOrEqualTo(100)
            .WithMessage("Number of copies cannot exceed 100 per request.");

        RuleFor(x => x.Isbn)
            .Isbn()
            .When(x => !string.IsNullOrWhiteSpace(x.Isbn));

        RuleFor(x => x.YearOfPublication)
            .GreaterThan(1000)
            .WithMessage("Year of publication must be a valid year.")
            .When(x => x.YearOfPublication.HasValue);

        RuleFor(x => x.Language)
            .MaximumLength(50)
            .WithMessage("Language cannot exceed 50 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Language));

        RuleFor(x => x.Publisher)
            .MaximumLength(255)
            .WithMessage("Publisher name cannot exceed 255 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Publisher));

        // Check for duplicate books based on ISBN or Title-Author combination
        RuleFor(x => x)
            .MustAsync(async (command, cancellationToken) =>
            {
                return !await bibRecordRepository.ExistsAsync(
                    x => x.InternationalStandardBookNumbers.Contains(command.Isbn) || (
                        x.TitleStatement.Title.Equals(command.Title, StringComparison.CurrentCultureIgnoreCase) &&
                        x.MainEntryPersonalName != null &&
                        x.MainEntryPersonalName.PersonalName.Equals(command.Author,
                            StringComparison.CurrentCultureIgnoreCase)),
                    cancellationToken);
            })
            .WithMessage(
                "A book with the same ISBN or Title-Author combination already exists. Consider adding more copies to the existing record instead.");
    }
}