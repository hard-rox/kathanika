using Kathanika.Application.Features.Publications.Queries;

namespace Kathanika.Application.Test.Queries;

public class GetPublicationByIdQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Return_Publication_With_Specific_Id()
    {
        Publication publication = Publication.Create(
            "Title",
            "",
            PublicationType.Book,
            "",
            DateOnly.MinValue,
            "",
            1,
            "",
            string.Empty,
            string.Empty
        );
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
