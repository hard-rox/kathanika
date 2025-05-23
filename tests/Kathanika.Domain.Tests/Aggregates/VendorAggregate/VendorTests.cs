using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Domain.Tests.Aggregates.VendorAggregate;

public class VendorTests
{
    private readonly Faker<Vendor> _vendorFaker;

    public VendorTests()
    {
        _vendorFaker = new Faker<Vendor>()
            .CustomInstantiator(f => Vendor.Create(
                f.Company.CompanyName(),
                f.Address.FullAddress(),
                f.Phone.PhoneNumber("###########"),
                f.Internet.Email(),
                f.Internet.Url(),
                f.Random.Word(),
                f.Person.FullName,
                f.Phone.PhoneNumber("###########"),
                f.Internet.Email()
            ).Value);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenNameIsEmpty()
    {
        // Arrange
        var emptyName = string.Empty;
        Vendor vendor = _vendorFaker.Generate();

        // Act
        KnResult<Vendor> result = Vendor.Create(
            emptyName,
            vendor.Address,
            vendor.ContactNumber,
            vendor.Email,
            vendor.Website,
            vendor.AccountDetail,
            vendor.ContactPersonName,
            vendor.ContactPersonPhone,
            vendor.ContactPersonEmail);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == VendorAggregateErrors.NameIsEmpty.Code);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenContactNumberIsInvalid()
    {
        // Arrange
        const string invalidPhoneNumber = "123abc";
        Vendor vendor = _vendorFaker.Generate();

        // Act
        KnResult<Vendor> result = Vendor.Create(
            vendor.Name,
            vendor.Address,
            invalidPhoneNumber,
            vendor.Email,
            vendor.Website,
            vendor.AccountDetail,
            vendor.ContactPersonName,
            vendor.ContactPersonPhone,
            vendor.ContactPersonEmail);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == VendorAggregateErrors.InvalidContactNumber.Code);
    }

    [Fact]
    public void Update_ShouldReturnFailure_WhenContactPersonPhoneIsInvalid()
    {
        // Arrange
        const string invalidPhoneNumber = "invalid-phone";
        Vendor vendor = _vendorFaker.Generate();

        // Act
        KnResult updateKnResult = vendor.Update(
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            invalidPhoneNumber,
            null);

        // Assert
        Assert.True(updateKnResult.IsFailure);
        Assert.Contains(updateKnResult.Errors, e => e.Code == VendorAggregateErrors.InvalidContactPersonPhone.Code);
    }

    [Fact]
    public void Update_ShouldUpdateFields_WhenValidDataProvided()
    {
        // Arrange
        Vendor vendor = _vendorFaker.Generate();
        const string newName = "New Vendor Name";

        // Act
        KnResult updateKnResult = vendor.Update(
            newName,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null);

        // Assert
        Assert.True(updateKnResult.IsSuccess);
        Assert.Equal(newName, vendor.Name);
    }

    [Fact]
    public void Create_ShouldReturnSuccess_WhenValidDataProvided()
    {
        // Arrange
        Vendor vendor = _vendorFaker.Generate();

        // Act
        KnResult<Vendor> result = Vendor.Create(
            vendor.Name,
            vendor.Address,
            vendor.ContactNumber,
            vendor.Email,
            vendor.Website,
            vendor.AccountDetail,
            vendor.ContactPersonName,
            vendor.ContactPersonPhone,
            vendor.ContactPersonEmail);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
}