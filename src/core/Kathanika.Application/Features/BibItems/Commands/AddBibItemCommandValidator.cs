using Kathanika.Domain.Aggregates.BibItemAggregate;
using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class AddBibItemCommandValidator : AbstractValidator<AddBibItemCommand>
{
    public AddBibItemCommandValidator(IBibItemRepository bibItemRepository, IBibRecordRepository bibRecordRepository)
    {
        RuleFor(x => x.BibRecordId)
            .NotEmpty()
            .WithMessage("BibRecord ID is required")
            .MustAsync(async (bibRecordId, cancellation) =>
                await bibRecordRepository.ExistsAsync(bibRecordId, cancellation))
            .WithMessage("BibRecord with this ID does not exist");

        RuleFor(x => x.Barcode)
            .NotEmpty()
            .WithMessage("Barcode is required")
            .MaximumLength(50)
            .WithMessage("Barcode must not exceed 50 characters")
            .MustAsync(async (barcode, cancellation) =>
            {
                return !await bibItemRepository.ExistsAsync(x => x.Barcode == barcode, cancellation);
            })
            .WithMessage("A BibItem with this barcode already exists");

        RuleFor(x => x.CallNumber)
            .NotEmpty()
            .WithMessage("Call number is required")
            .MaximumLength(100)
            .WithMessage("Call number must not exceed 100 characters");

        RuleFor(x => x.Location)
            .MaximumLength(100)
            .WithMessage("Location must not exceed 100 characters");

        RuleFor(x => x.ItemType)
            .IsInEnum()
            .WithMessage("Invalid item type");

        RuleFor(x => x.ConditionNote)
            .MaximumLength(500)
            .WithMessage("Condition note must not exceed 500 characters")
            .When(x => !string.IsNullOrEmpty(x.ConditionNote));

        RuleFor(x => x.Notes)
            .MaximumLength(1000)
            .WithMessage("Notes must not exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.Notes));
    }
}