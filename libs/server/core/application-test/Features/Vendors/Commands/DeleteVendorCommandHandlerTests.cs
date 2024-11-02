using Kathanika.Core.Application.Features.Vendors.Commands;
using Kathanika.Core.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Core.Application.Test.Features.Vendors.Commands;

public class DeleteVendorCommandHandlerTests
{
    [Fact]
    public async Task Handler_ShouldCallDeleteAsync()
    {
        string id = Guid.NewGuid().ToString();
        Vendor vendor = new Faker<Vendor>().CustomInstantiator(f => Vendor.Create(
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
        string id = Guid.NewGuid().ToString();
        IVendorRepository vendorRepository = Substitute.For<IVendorRepository>();
        DeleteVendorCommand command = new(id);
        DeleteVendorCommandHandler handler = new(vendorRepository);

        Result result = await handler.Handle(command, default);

        Assert.True(result.IsFailure);
        Assert.Equal(result.Errors.FirstOrDefault(), VendorAggregateErrors.NotFound(id));
    }
}