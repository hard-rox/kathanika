namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class AddBibItemCommandValidator : AbstractValidator<AddBibItemCommand>
{
    public AddBibItemCommandValidator()
    {
        RuleFor(x => x.BibRecordId)
            .NotEmpty()
            .WithMessage("BibRecord ID is required");

        RuleFor(x => x.Barcode)
            .NotEmpty()
            .WithMessage("Barcode is required")
            .MaximumLength(50)
            .WithMessage("Barcode must not exceed 50 characters");

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

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid item status");

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