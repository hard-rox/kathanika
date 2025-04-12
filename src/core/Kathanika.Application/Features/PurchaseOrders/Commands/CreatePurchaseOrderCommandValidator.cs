using Kathanika.Application.CommonValidators;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.PurchaseOrders.Commands;

internal sealed class CreatePurchaseOrderCommandValidator : AbstractValidator<CreatePurchaseOrderCommand>
{
    public CreatePurchaseOrderCommandValidator(IVendorRepository vendorRepository)
    {
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("Items cannot be empty.");

        RuleFor(x => x.VendorId)
            .NotEmpty()
            .MustAsync(
                async (vendorId, cancellationToken)
                    => await vendorRepository.ExistsAsync(vendorId, cancellationToken))
            .WithMessage("Vendor ID cannot be empty.");

        RuleForEach(x => x.Items).SetValidator(new PurchaseItemDtoValidator());
    }
}

internal class PurchaseItemDtoValidator : AbstractValidator<PurchaseItemDto>
{
    public PurchaseItemDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.Isbn)
            .Isbn()
            .When(x => !string.IsNullOrEmpty(x.Isbn));

        RuleFor(x => x.VendorPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Vendor Price must be non-negative.")
            .When(x => x.VendorPrice.HasValue);
    }
}