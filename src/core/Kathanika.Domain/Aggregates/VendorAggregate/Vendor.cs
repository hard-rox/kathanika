using System.Net.Mail;
using System.Text.RegularExpressions;
using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.VendorAggregate;

public sealed class Vendor : AggregateRoot
{
    private Vendor(
        string name,
        string address,
        string contactNumber
    )
    {
        Name = name;
        Address = address;
        ContactNumber = contactNumber;
    }

    public string Name { get; private set; }
    public string Address { get; private set; }
    public string ContactNumber { get; private set; }
    public string? Email { get; private set; }
    public string? Website { get; private set; }
    public string? AccountDetail { get; private set; }
    public string? ContactPersonName { get; private set; }
    public string? ContactPersonPhone { get; private set; }
    public string? ContactPersonEmail { get; private set; }
    public VendorStatus Status { get; private set; }

    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^\+?\d{1,14}$");
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            MailAddress addr = new(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }

    public static Result<Vendor> Create(
        string name,
        string address,
        string contactNumber,
        string? email,
        string? website,
        string? accountDetail,
        string? contactPersonName,
        string? contactPersonPhone,
        string? contactPersonEmail,
        VendorStatus status
    )
    {
        List<KnError> errors = [];

        if (string.IsNullOrWhiteSpace(name))
            errors.Add(VendorAggregateErrors.NameIsEmpty);

        if (string.IsNullOrWhiteSpace(address))
            errors.Add(VendorAggregateErrors.AddressIsEmpty);

        if (!IsValidPhoneNumber(contactNumber))
            errors.Add(VendorAggregateErrors.InvalidContactNumber);

        if (email is not null && !IsValidEmail(email))
            errors.Add(VendorAggregateErrors.InvalidEmail);

        if (website is not null && !IsValidUrl(website))
            errors.Add(VendorAggregateErrors.InvalidWebsiteUrl);

        if (contactPersonPhone is not null && !IsValidPhoneNumber(contactPersonPhone))
            errors.Add(VendorAggregateErrors.InvalidContactPersonPhone);

        if (contactPersonEmail is not null && !IsValidEmail(contactPersonEmail))
            errors.Add(VendorAggregateErrors.InvalidContactPersonEmail);

        if (errors.Count > 0)
            return Result.Failure<Vendor>(errors);

        Vendor newVendor = new(
            name,
            address,
            contactNumber
        )
        {
            Email = email,
            Website = website,
            AccountDetail = accountDetail,
            ContactPersonName = contactPersonName,
            ContactPersonPhone = contactPersonPhone,
            ContactPersonEmail = contactPersonEmail,
            Status = status
        };

        return Result.Success(newVendor);
    }

    public Result Update(
        string? name,
        string? address,
        string? contactNumber,
        string? email,
        string? website,
        string? accountDetail,
        string? contactPersonName,
        string? contactPersonPhone,
        string? contactPersonEmail,
        VendorStatus? status
    )
    {
        List<KnError> errors = [];

        if (name is not null && name.Length == 0)
            errors.Add(VendorAggregateErrors.NameIsEmpty);

        if (address is not null && address.Length == 0)
            errors.Add(VendorAggregateErrors.AddressIsEmpty);

        if (contactNumber is not null && !IsValidPhoneNumber(contactNumber))
            errors.Add(VendorAggregateErrors.InvalidContactNumber);

        if (email is not null && !IsValidEmail(email))
            errors.Add(VendorAggregateErrors.InvalidEmail);

        if (website is not null && !IsValidUrl(website))
            errors.Add(VendorAggregateErrors.InvalidWebsiteUrl);

        if (contactPersonPhone is not null && !IsValidPhoneNumber(contactPersonPhone))
            errors.Add(VendorAggregateErrors.InvalidContactPersonPhone);

        if (contactPersonEmail is not null && !IsValidEmail(contactPersonEmail))
            errors.Add(VendorAggregateErrors.InvalidContactPersonEmail);

        if (errors.Count > 0)
            return Result.Failure<Vendor>(errors);

        Name = name ?? Name;
        Address = address ?? Address;
        ContactNumber = contactNumber ?? ContactNumber;
        Email = email ?? Email;
        Website = website ?? Website;
        AccountDetail = accountDetail ?? AccountDetail;
        ContactPersonName = contactPersonName ?? ContactPersonName;
        ContactPersonPhone = contactPersonPhone ?? ContactPersonPhone;
        ContactPersonEmail = contactPersonEmail ?? ContactPersonEmail;
        Status = status ?? Status;

        return Result.Success();
    }
}