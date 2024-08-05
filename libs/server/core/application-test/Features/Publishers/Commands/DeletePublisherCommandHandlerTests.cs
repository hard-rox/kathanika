using System.Linq.Expressions;
using Kathanika.Core.Application.Features.Publishers.Commands;

namespace Kathanika.Core.Application.Test.Features.Publishers.Commands;

public class DeletePublisherCommandHandlerTests
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IPublicationRepository _publicationRepository;
    private readonly DeletePublisherCommandHandler _handler;

    public DeletePublisherCommandHandlerTests()
    {
        _publisherRepository = Substitute.For<IPublisherRepository>();
        _publicationRepository = Substitute.For<IPublicationRepository>();
        _handler = new DeletePublisherCommandHandler(_publisherRepository, _publicationRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFoundError_WhenPublisherNotFound()
    {
        // Arrange
        DeletePublisherCommand command = new(Guid.NewGuid().ToString());
        _publisherRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Publisher?>(null));

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(PublisherAggregateErrors.NotFound(command.Id), result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnHasPublicationError_WhenPublisherHasPublication()
    {
        // Arrange
        Publisher dummy = new Faker<Publisher>()
            .CustomInstantiator(f => Publisher.Create(
                f.Company.CompanyName(),
                f.Lorem.Paragraph(),
                f.Address.FullAddress()
            ).Value)
            .Generate();
        DeletePublisherCommand command = new(Guid.NewGuid().ToString());
        _publisherRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Publisher?>(dummy));
        _publicationRepository.ExistsAsync(Arg.Any<Expression<Func<Publication, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true));

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(PublisherAggregateErrors.HasPublication, result.Errors);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenDeletionIsSuccessful()
    {
        // Arrange
        Publisher dummy = new Faker<Publisher>()
            .CustomInstantiator(f => Publisher.Create(
                f.Company.CompanyName(),
                f.Lorem.Paragraph(),
                f.Address.FullAddress()
            ).Value)
            .Generate();
        DeletePublisherCommand command = new(Guid.NewGuid().ToString());
        _publisherRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Publisher?>(dummy));
        _publicationRepository.ExistsAsync(Arg.Any<Expression<Func<Publication, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(false));

        // Act
        Result result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _publisherRepository.Received(1).DeleteAsync(command.Id, Arg.Any<CancellationToken>());
    }
}
