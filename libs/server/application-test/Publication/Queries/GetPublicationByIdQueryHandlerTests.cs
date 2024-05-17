using Bogus;
using Kathanika.Application.Features.Publications.Queries;

namespace Kathanika.Application.Test.Queries;

public class GetPublicationByIdQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Return_Publication_With_Specific_Id()
    {
        Publication publication = new Faker<Publication>()
            .CustomInstantiator(factoryMethod => Publication.Create(
                factoryMethod.Lorem.Sentence(),
                factoryMethod.Random.AlphaNumeric(8),
                factoryMethod.Random.Enum<PublicationType>(),
                factoryMethod.Company.CompanyName(),
                factoryMethod.Date.PastDateOnly(),
                "",
                factoryMethod.Random.AlphaNumeric(5),
                factoryMethod.Lorem.Sentences(5),
                factoryMethod.Locale,
                AcquisitionMethod.Purchase,
                factoryMethod.Random.Number(100),
                factoryMethod.Random.Decimal(1000),
                null,
                null
            ));
        string id = Guid.NewGuid().ToString();
        GetPublicationByIdQuery query = new(id);
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.GetByIdAsync(Arg.Any<string>(), default).Returns(publication);
        GetPublicationByIdQueryHandler handler = new(publicationRepository);

        Publication? returnedPublication = await handler.Handle(query, default);

        await publicationRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
        Assert.NotNull(returnedPublication);
        Assert.Equal(publication.Title, returnedPublication.Title);
    }
}
