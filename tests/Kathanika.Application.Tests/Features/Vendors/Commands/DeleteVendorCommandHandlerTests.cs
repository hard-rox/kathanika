using Kathanika.Application.Features.Vendors.Commands;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Tests.Features.Vendors.Commands;

public class DeleteVendorCommandHandlerTests
{
    [Fact]
    public async Task Handler_ShouldCallDeleteAsync()
    {
        var id = Guid.NewGuid().ToString();
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
        vendorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(vendor);
        DeleteVendorCommand command = new(id);
        DeleteVendorCommandHandler handler = new(vendorRepository);

        await handler.Handle(command, default);

        await vendorRepository.Received(1)
            .DeleteAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handler_ShouldReturnFailureResult_WhenInvalidVendorId()
    {
        var id = Guid.NewGuid().ToString();
        IVendorRepository vendorRepository = Substitute.For<IVendorRepository>();
        DeleteVendorCommand command = new(id);
        DeleteVendorCommandHandler handler = new(vendorRepository);

        KnResult knResult = await handler.Handle(command, default);

        Assert.True(knResult.IsFailure);
        Assert.Equal(knResult.Errors.FirstOrDefault(), VendorAggregateErrors.NotFound(id));
    }
}