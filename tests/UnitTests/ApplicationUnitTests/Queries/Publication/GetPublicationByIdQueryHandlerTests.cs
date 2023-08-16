using Kathanika.Application.Queries;

namespace Kathanika.UnitTests.ApplicationUnitTests.Queries;

public class GetPublicationByIdQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Return_Publication_With_Specific_Id()
    {
        var publication = Publication.Create(
            "Title",
            "",
            PublicationType.Book,
            "",
            DateOnly.MinValue,
            (decimal)102.0,
            1,
            ""
        );
        var id = Guid.NewGuid().ToString();
        var query = new GetPublicationByIdQuery(id);
        var publicationRepository = Substitute.For<IPublicationRepository>();
        publicationRepository.GetByIdAsync(Arg.Any<string>(), default).Returns(publication);
        var handler = new GetPublicationByIdQueryHandler(publicationRepository);

        var returnedPublication = await handler.Handle(query, default);

        await publicationRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
        Assert.NotNull(returnedPublication);
        Assert.Equal(publication.Title, returnedPublication.Title);
    }
}