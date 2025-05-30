using System.Linq.Expressions;
using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.PurchaseOrders.Commands;

internal sealed class UpdatePurchaseOrderCommandValidator : AbstractValidator<UpdatePurchaseOrderCommand>
{
    public UpdatePurchaseOrderCommandValidator(IPurchaseOrderRepository purchaseOrderRepository, IVendorRepository vendorRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(purchaseOrderRepository.ExistsAsync)
            .WithMessage("Invalid order");

        RuleFor(x => x.Patch.VendorId)
            .MustAsync(vendorRepository.ExistsAsync!)
            .When(x => x.Patch.VendorId is not null)
            .WithMessage("Invalid order");

        RuleFor(x => new { x.Id, x.Patch })
            .MustAsync(async (props, cancellationToken) =>
            {
                Expression<Func<PurchaseOrder, bool>> expression = p =>
                    p.Id != props.Id
                    && p.VendorId == props.Patch.VendorId;
                var isDuplicate = await purchaseOrderRepository.ExistsAsync(expression, cancellationToken);
                return !isDuplicate;
            })
            .WithName("ContactNumber, Email, CardNumber")
            .WithMessage(
                "Duplicate patron information {PropertyName}. Consider updating existing publication's 'Copies Available' field."); ;
    }
}