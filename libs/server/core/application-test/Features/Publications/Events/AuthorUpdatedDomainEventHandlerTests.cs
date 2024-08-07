using Kathanika.Core.Application.Features.Publications.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute.ReturnsExtensions;

namespace Kathanika.Core.Application.Test.Features.Publications.Events;

public sealed class AuthorUpdatedDomainEventHandlerTests
{
    private readonly ILogger<AuthorUpdatedDomainEventHandler> _nullLogger = new NullLogger<AuthorUpdatedDomainEventHandler>();

    [Fact]
    public async Task Handler_ShouldReturn_WhenNoAuthorFound()
    {
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        AuthorUpdatedDomainEventHandler handler = new(_nullLogger, authorRepository, publicationRepository);
        AuthorUpdatedDomainEvent authorUpdatedDomainEvent = new("12345");
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).ReturnsNull();

        await handler.Handle(authorUpdatedDomainEvent, default);

        await publicationRepository.DidNotReceive().ListAllByAuthorIdAsync(Arg.Any<string>());
    }

    [Fact]
    public async Task Handler_ShouldUpdateAllPublication_WhenAuthorFound()
    {
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        IPublicationRepository publicationRepository = Substitute.For<IPublicationRepository>();
        string dummyId = "12345";
        Author author = new Faker<Author>()
            .CustomInstantiator(factoryMethod => Author.Create(
                factoryMethod.Name.FirstName(),
                factoryMethod.Name.LastName(),
                factoryMethod.Date.PastDateOnly(),
                null,
                factoryMethod.Address.Country(),
                factoryMethod.Lorem.Paragraph(5)
            ).Value);

        List<Publication> publications = new Faker<Publication>()
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
                factoryMethod.Random.Number(1, 100),
                null,
                factoryMethod.Random.Decimal(1, 1000),
                factoryMethod.Random.Word(),
                null
            ).Value).GenerateBetween(2, 10);

        AuthorUpdatedDomainEventHandler handler = new(_nullLogger, authorRepository, publicationRepository);
        AuthorUpdatedDomainEvent authorUpdatedDomainEvent = new(dummyId);
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(author);
        publicationRepository.ListAllByAuthorIdAsync(Arg.Any<string>())
            .Returns(publications);

        await handler.Handle(authorUpdatedDomainEvent, default);

        await publicationRepository.Received(1).ListAllByAuthorIdAsync(Arg.Is<string>(x => x == dummyId));
        await publicationRepository.Received(publications.Count).UpdateAsync(Arg.Is<Publication>(x => publications.Contains(x)));
    }
}
