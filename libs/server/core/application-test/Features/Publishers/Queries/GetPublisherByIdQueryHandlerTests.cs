using Kathanika.Core.Application.Features.Publishers.Queries;

namespace Kathanika.Core.Application.Test.Features.Publishers.Queries;

public class GetPublisherByIdQueryHandlerTests
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly GetPublisherByIdQueryHandler _handler;
    private readonly Faker<Publisher> _publisherFaker;

    public GetPublisherByIdQueryHandlerTests()
    {
        _publisherRepository = Substitute.For<IPublisherRepository>();
        _handler = new GetPublisherByIdQueryHandler(_publisherRepository);
        _publisherFaker = new Faker<Publisher>()
            .CustomInstantiator(f => Publisher.Create(
                f.Company.CompanyName(),
                f.Lorem.Paragraph(),
                f.Address.FullAddress()).Value);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenPublisherNotFound()
    {
        // Arrange
        GetPublisherByIdQuery query = new(Guid.NewGuid().ToString());
        _publisherRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Publisher?>(null));

        // Act
        Result<Publisher> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(PublisherAggregateErrors.NotFound(query.Id), result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenPublisherIsFound()
    {
        // Arrange
        Publisher publisher = _publisherFaker.Generate();
        _publisherRepository.GetByIdAsync(publisher.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Publisher?>(publisher));

        GetPublisherByIdQuery query = new(publisher.Id);

        // Act
        Result<Publisher> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(publisher.Id, result.Value.Id);
        Assert.Equal(publisher.Name, result.Value.Name);
        Assert.Equal(publisher.Description, result.Value.Description);
        Assert.Equal(publisher.ContactInformation, result.Value.ContactInformation);
    }
}
