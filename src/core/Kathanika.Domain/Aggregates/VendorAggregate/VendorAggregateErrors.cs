using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.VendorAggregate;

public static class VendorAggregateErrors
{
    public static KnError NameIsEmpty => new(
        "Vendor.NameIsEmpty",
        "Vendor Name must be unique and not empty."
    );

    public static KnError AddressIsEmpty => new(
        "Vendor.AddressIsEmpty",
        "Address cannot be empty."
    );

    public static KnError InvalidContactNumber => new(
        "Vendor.InvalidContactNumber",
        "Invalid contact number."
    );

    public static KnError InvalidEmail => new(
        "Vendor.InvalidEmail",
        "Invalid email address."
    );

    public static KnError InvalidWebsiteUrl => new(
        "Vendor.InvalidWebsiteUrl",
        "Invalid website URL."
    );

    public static KnError InvalidContactPersonPhone => new(
        "Vendor.InvalidContactPersonPhone",
        "Invalid contact person phone number."
    );

    public static KnError InvalidContactPersonEmail => new(
        "Vendor.InvalidContactPersonEmail",
        "Invalid contact person email address."
    );

    public static KnError NotFound(string id) => new(
        "Vendor.NotFound",
        message: $"No Vendor found with this Id: \"{id}\""
    );
}