using Kathanika.Core.Application.CommonValidators;

namespace Kathanika.Core.Application.Features.Patrons.Commands;

internal sealed class CreatePatronCommandValidator : AbstractValidator<CreatePatronCommand>
{
    public CreatePatronCommandValidator(
        IPatronRepository patronRepository
    )
    {
        RuleFor(x => x.Surname)
            .NotNull()
            .NotEmpty()
            .WithMessage("Surname is required.");

        RuleFor(x => x.CardNumber)
            .NotNull()
            .NotEmpty()
            .MustAsync(
                async (cardNumber, cancellationToken)
                => await patronRepository.ExistsAsync(x => x.CardNumber == cardNumber, cancellationToken)
            ).WithMessage("Card Number must be unique and not empty.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date of birth cannot be in the future.");

        RuleFor(x => x.ContactNumber)
            .ContactNumber()
            .When(x => x.ContactNumber is not null);
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => x.Email is not null);
    }
}
