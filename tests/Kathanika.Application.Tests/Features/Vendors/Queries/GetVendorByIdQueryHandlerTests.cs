using Kathanika.Application.Features.Vendors.Queries;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Tests.Features.Vendors.Queries;

public class GetVendorByIdQueryHandlerTests
{
    private readonly IVendorRepository _vendorRepository;
    private readonly GetVendorByIdQueryHandler _handler;

    public GetVendorByIdQueryHandlerTests()
    {
        _vendorRepository = Substitute.For<IVendorRepository>();
        _handler = new GetVendorByIdQueryHandler(_vendorRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenVendorNotFound()
    {
        // Arrange
        GetVendorByIdQuery query = new(Guid.NewGuid().ToString());
        _vendorRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Vendor?>(null));

        // Act
        Result<Vendor> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(VendorAggregateErrors.NotFound(query.Id), result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenVendorFound()
    {
        // Arrange
        var vendorId = Guid.NewGuid().ToString();
        Vendor vendor = new Faker<Vendor>()
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
            ).Value);

        GetVendorByIdQuery query = new(vendorId);
        _vendorRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Vendor?>(vendor));

        // Act
        Result<Vendor> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(vendor.Email, result.Value.Email);
    }
}