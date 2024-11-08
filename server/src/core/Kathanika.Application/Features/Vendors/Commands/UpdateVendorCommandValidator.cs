using System.Linq.Expressions;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.Vendors.Commands;

internal sealed class UpdateVendorCommandValidator : AbstractValidator<UpdateVendorCommand>
{
    public UpdateVendorCommandValidator(IVendorRepository vendorRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(vendorRepository.ExistsAsync)
            .WithMessage("Invalid Vendor");
        RuleFor(x => new { x.Id, x.Patch })
            .MustAsync(async (props, cancellationToken) =>
            {
                Expression<Func<Vendor, bool>> expression = a =>
                    a.Id != props.Id
                    && a.Name == props.Patch.Name
                    && a.Address == props.Patch.Address
                    && a.ContactNumber == props.Patch.ContactNumber;
                bool isDuplicate = await vendorRepository.ExistsAsync(expression, cancellationToken);
                return !isDuplicate;
            })
            .WithName("Name, Address, Contact Number")
            .WithMessage("Duplicate vendor information {PropertyName}");

        RuleFor(x => x.Patch)
            .SetValidator(new VendorPatchValidator(vendorRepository));
    }
}

internal class VendorPatchValidator : AbstractValidator<VendorPatch>
{
    public VendorPatchValidator(IVendorRepository vendorRepository)
    {
        RuleFor(v => v.Name)
            .NotEmpty().WithMessage("Vendor Name must be unique and not empty.")
            .MustAsync(
                async (name, cancellationToken)
                => await vendorRepository.ExistsAsync(x => x.Name == name, cancellationToken)
            ).WithMessage("Vendor Name must be unique and not empty.");

        RuleFor(v => v.Address)
            .NotEmpty().WithMessage("Address cannot be empty.");

        RuleFor(v => v.ContactNumber)
            .NotEmpty().WithMessage("Invalid contact number.")
            .Matches(@"^\+?\d{1,14}$").WithMessage("Invalid contact number.");

        RuleFor(v => v.Email)
            .EmailAddress().WithMessage("Invalid email address.")
            .When(v => !string.IsNullOrEmpty(v.Email));

        RuleFor(v => v.Website)
            .Must((url) => Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
            ).WithMessage("Invalid website URL.")
            .When(v => !string.IsNullOrEmpty(v.Website));

        RuleFor(v => v.ContactPersonName)
            .NotEmpty().WithMessage("Contact Person Name cannot be empty.");

        RuleFor(v => v.ContactPersonPhone)
            .NotEmpty().WithMessage("Invalid contact person phone number.")
            .Matches(@"^\+?\d{1,14}$").WithMessage("Invalid contact person phone number.");

        RuleFor(v => v.ContactPersonEmail)
            .EmailAddress().WithMessage("Invalid contact person email address.")
            .When(v => !string.IsNullOrEmpty(v.ContactPersonEmail));
    }
}
