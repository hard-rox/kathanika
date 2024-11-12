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
                f.Internet.Email(),
                VendorStatus.Active
            ).Value!);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenNameIsEmpty()
    {
        // Arrange
        string emptyName = string.Empty;
        Vendor vendor = _vendorFaker.Generate();

        // Act
        Result<Vendor> result = Vendor.Create(
            emptyName,
            vendor.Address,
            vendor.ContactNumber,
            vendor.Email,
            vendor.Website,
            vendor.AccountDetail,
            vendor.ContactPersonName,
            vendor.ContactPersonPhone,
            vendor.ContactPersonEmail,
            VendorStatus.Active);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == VendorAggregateErrors.NameIsEmpty.Code);
    }

    [Fact]
    public void Create_ShouldReturnFailure_WhenContactNumberIsInvalid()
    {
        // Arrange
        string invalidPhoneNumber = "123abc";
        Vendor vendor = _vendorFaker.Generate();

        // Act
        Result<Vendor> result = Vendor.Create(
            vendor.Name,
            vendor.Address,
            invalidPhoneNumber,
            vendor.Email,
            vendor.Website,
            vendor.AccountDetail,
            vendor.ContactPersonName,
            vendor.ContactPersonPhone,
            vendor.ContactPersonEmail,
            VendorStatus.Active);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(result.Errors, e => e.Code == VendorAggregateErrors.InvalidContactNumber.Code);
    }

    [Fact]
    public void Update_ShouldReturnFailure_WhenContactPersonPhoneIsInvalid()
    {
        // Arrange
        string invalidPhoneNumber = "invalid-phone";
        Vendor vendor = _vendorFaker.Generate();

        // Act
        Result updateResult = vendor.Update(
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            invalidPhoneNumber,
            null,
            null);

        // Assert
        Assert.True(updateResult.IsFailure);
        Assert.Contains(updateResult.Errors, e => e.Code == VendorAggregateErrors.InvalidContactPersonPhone.Code);
    }

    [Fact]
    public void Update_ShouldUpdateFields_WhenValidDataProvided()
    {
        // Arrange
        Vendor vendor = _vendorFaker.Generate();
        string newName = "New Vendor Name";

        // Act
        Result updateResult = vendor.Update(
            newName,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            VendorStatus.Active);

        // Assert
        Assert.True(updateResult.IsSuccess);
        Assert.Equal(newName, vendor.Name);
    }

    [Fact]
    public void Create_ShouldReturnSuccess_WhenValidDataProvided()
    {
        // Arrange
        Vendor vendor = _vendorFaker.Generate();

        // Act
        Result<Vendor> result = Vendor.Create(
            vendor.Name,
            vendor.Address,
            vendor.ContactNumber,
            vendor.Email,
            vendor.Website,
            vendor.AccountDetail,
            vendor.ContactPersonName,
            vendor.ContactPersonPhone,
            vendor.ContactPersonEmail,
            VendorStatus.Active);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
    }
}