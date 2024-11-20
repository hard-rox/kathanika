using Kathanika.Application.CommonValidators;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.Vendors.Commands;

// ReSharper disable once UnusedType.Global
internal sealed class AddVendorCommandValidator : AbstractValidator<AddVendorCommand>
{
    public AddVendorCommandValidator(IVendorRepository vendorRepository)
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MustAsync(
                async (name, cancellationToken)
                    => !await vendorRepository.ExistsAsync(x => x.Name == name, cancellationToken)
            ).WithMessage("Vendor Name must be unique and not empty.");

        RuleFor(v => v.Address)
            .NotEmpty().WithMessage("Address cannot be empty.");

        RuleFor(v => v.ContactNumber)
            .NotEmpty()
            .ContactNumber()
            .WithMessage("Invalid contact number.");

        RuleFor(v => v.Email)
            .EmailAddress().WithMessage("Invalid email address.")
            .When(v => !string.IsNullOrEmpty(v.Email));

        RuleFor(v => v.Website)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) &&
                           (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
            ).WithMessage("Invalid website URL.")
            .When(v => !string.IsNullOrEmpty(v.Website));

        RuleFor(v => v.ContactPersonName)
            .NotEmpty().WithMessage("Contact Person Name cannot be empty.");

        RuleFor(v => v.ContactPersonPhone)
            .NotEmpty()
            .ContactNumber()
            .WithMessage("Invalid contact person phone number.");

        RuleFor(v => v.ContactPersonEmail)
            .EmailAddress().WithMessage("Invalid contact person email address.")
            .When(v => !string.IsNullOrEmpty(v.ContactPersonEmail));
    }
}