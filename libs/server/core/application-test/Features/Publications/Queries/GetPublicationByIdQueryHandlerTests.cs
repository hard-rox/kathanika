using Kathanika.Core.Application.Features.Publications.Queries;

namespace Kathanika.Core.Application.Test.Features.Publications.Queries;

public class GetPublicationByIdQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_ReturnPublicationWithSpecificId()
    {
        Publication publication = new Faker<Publication>()
            .CustomInstantiator(factoryMethod => Publication.Create(
                factoryMethod.Lorem.Sentence(),
                factoryMethod.Random.AlphaNumeric(8),
                factoryMethod.Random.Enum<PublicationType>(),
                factoryMethod.Date.PastDateOnly(),
                "",
                "",
                factoryMethod.Random.AlphaNumeric(5),
                factoryMethod.Lorem.Sentences(5),
                factoryMethod.Locale,
                AcquisitionMethod.Purchase,
                factoryMethod.Random.Number(100),
                null,
                factoryMethod.Random.Decimal(1000),
                factoryMethod.Random.Word(),
                null
            ).Value);
        string id = Guid.NewGuid().ToString();
        GetPublicationByIdQuery query = new(id);
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.GetByIdAsync(Arg.Any<string>(), default).Returns(publication);
        GetPublicationByIdQueryHandler handler = new(publicationRepository);

        Result<Publication> result = await handler.Handle(query, default);

        await publicationRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(publication.Title, result.Value.Title);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenPublicationNotFound()
    {
        // Arrange
        GetPublicationByIdQuery query = new(Guid.NewGuid().ToString());
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.GetByIdAsync(query.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult<Publication?>(null));
        GetPublicationByIdQueryHandler handler = new(publicationRepository);

        // Act
        Result<Publication> result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.NotNull(result.Errors);
        Assert.Contains(PublicationAggregateErrors.NotFound(query.Id), result.Errors);
    }
}
