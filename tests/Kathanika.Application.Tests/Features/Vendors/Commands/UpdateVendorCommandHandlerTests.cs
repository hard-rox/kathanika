using Kathanika.Application.Features.Vendors.Commands;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Tests.Features.Vendors.Commands;

public class UpdateVendorCommandHandlerTests
{
    [Fact]
    public async Task Handler_ShouldReturnFailureResult_WhenInvalidVendorId()
    {
        var id = Guid.NewGuid().ToString();
        IVendorRepository vendorRepository = Substitute.For<IVendorRepository>();
        UpdateVendorCommand command = new(
            id,
            new VendorPatch("UpdatedName")
        );
        UpdateVendorCommandHandler handler = new(vendorRepository);

        KnResult knResult = await handler.Handle(command, default);

        Assert.True(knResult.IsFailure);
        Assert.Equal(knResult.Errors.FirstOrDefault(), VendorAggregateErrors.NotFound(id));
    }

    [Fact]
    public async Task Handler_ShouldCallUpdateAsync_WithValidInput()
    {
        var vendorId = Guid.NewGuid().ToString();
        Vendor vendor = new Faker<Vendor>().CustomInstantiator(f => Vendor.Create(
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
        IVendorRepository vendorRepository = Substitute.For<IVendorRepository>();
        vendorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(vendor);
        await vendorRepository.UpdateAsync(Arg.Any<Vendor>(), Arg.Any<CancellationToken>());
        UpdateVendorCommand command = new(vendorId, new VendorPatch(
            "Updated Name"
        ));
        UpdateVendorCommandHandler handler = new(vendorRepository);

        KnResult<Vendor> updatedVendor = await handler.Handle(command, default);

        Assert.True(updatedVendor.IsSuccess);
        Assert.Equal("Updated Name", updatedVendor.Value.Name);
        await vendorRepository.Received(1).GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>());
        await vendorRepository.Received(1).UpdateAsync(Arg.Is<Vendor>(x => x == vendor), Arg.Any<CancellationToken>());
    }
}