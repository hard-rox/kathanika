using Kathanika.Application.Features.Vendors.Commands;
using Kathanika.Domain.Aggregates.VendorAggregate;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Kathanika.Application.Tests.Features.Vendors.Commands;

public sealed class AddVendorCommandHandlerTests
{
    private readonly ILogger<AddVendorCommandHandler> _logger = new NullLogger<AddVendorCommandHandler>();
    private readonly IVendorRepository _vendorRepository = Substitute.For<IVendorRepository>();

    [Fact]
    public async Task Handler_ShouldReturnVendor_OnSavingNewVendor()
    {
        // Arrange
        Vendor dummyVendor = new Faker<Vendor>().CustomInstantiator(f => Vendor.Create(
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
        ).Value);
        AddVendorCommand command = new(
            dummyVendor.Name,
            dummyVendor.Address,
            dummyVendor.ContactNumber,
            dummyVendor.Email,
            dummyVendor.Website,
            dummyVendor.AccountDetail,
            dummyVendor.ContactPersonName,
            dummyVendor.ContactPersonPhone,
            dummyVendor.ContactPersonEmail,
            dummyVendor.Status
        );
        AddVendorCommandHandler handler = new(_logger, _vendorRepository);

        _vendorRepository.AddAsync(Arg.Any<Vendor>(), Arg.Any<CancellationToken>())
            .Returns(dummyVendor);

        // Act
        Result<Vendor> result = await handler.Handle(command, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(dummyVendor.Name, result.Value.Name);
    }
}