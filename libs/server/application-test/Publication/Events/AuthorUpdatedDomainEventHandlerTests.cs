using Bogus;
using Kathanika.Application.Features.Publications.Events;
using Kathanika.Domain.DomainEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute.ReturnsExtensions;

namespace Kathanika.Application.Test.Events;

public sealed class AuthorUpdatedDomainEventHandlerTests
{
    private readonly ILogger<AuthorUpdatedDomainEventHandler> _nullLogger = new NullLogger<AuthorUpdatedDomainEventHandler>();

    [Fact]
    public async Task Handler_Should_Return_When_No_Author_Found()
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
    public async Task Handler_Should_Update_All_Publication_When_Author_Found()
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
            ));

        List<Publication> publications = new Faker<Publication>()
            .CustomInstantiator(factoryMethod => Publication.Create(
                factoryMethod.Lorem.Sentence(),
                factoryMethod.Random.AlphaNumeric(8),
                factoryMethod.Random.Enum<PublicationType>(),
                factoryMethod.Company.CompanyName(),
                factoryMethod.Date.PastDateOnly(),
                "",
                factoryMethod.Random.Number(1, 5),
                factoryMethod.Random.AlphaNumeric(5),
                factoryMethod.Lorem.Sentences(5),
                factoryMethod.Locale,
                null
            )).GenerateBetween(2, 10);

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
