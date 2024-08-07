using Kathanika.Core.Application.Features.Publishers.Commands;

namespace Kathanika.Core.Application.Test.Features.Publishers.Commands;

public class UpdatePublisherCommandHandlerTests
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly UpdatePublisherCommandHandler _handler;
    private readonly Faker<Publisher> _publisherFaker;

    public UpdatePublisherCommandHandlerTests()
    {
        _publisherRepository = Substitute.For<IPublisherRepository>();
        _handler = new UpdatePublisherCommandHandler(_publisherRepository);
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
        UpdatePublisherCommand command = new(
            Guid.NewGuid().ToString(),
            new PublisherPatch(
                "New Name",
                "New Description",
                "New Contact Information")
        );
        _publisherRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Publisher?>(null));

        // Act
        Result<Publisher> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(PublisherAggregateErrors.NotFound(command.Id), result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenUpdateSucceeds()
    {
        // Arrange
        Publisher publisher = _publisherFaker.Generate();
        _publisherRepository.GetByIdAsync(publisher.Id, Arg.Any<CancellationToken>()).Returns(Task.FromResult<Publisher?>(publisher));

        UpdatePublisherCommand command = new(
            publisher.Id,
            new PublisherPatch(
                "Updated Name",
                "Updated Description",
                "Updated Contact Information")
        );

        // Act
        Result<Publisher> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(publisher.Id, result.Value.Id);
        Assert.Equal("Updated Name", result.Value.Name);
        Assert.Equal("Updated Description", result.Value.Description);
        Assert.Equal("Updated Contact Information", result.Value.ContactInformation);
    }
}
