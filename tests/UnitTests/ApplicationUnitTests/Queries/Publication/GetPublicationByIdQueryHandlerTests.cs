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
            DateTime.Today,
            (decimal)102.0,
            1,
            ""
        );
        var id = Guid.NewGuid().ToString();
        var query = new GetPublicationByIdQuery(id);
        var publicationRepositoryMock = new Mock<IPublicationRepository>();
        publicationRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(publication)
            .Verifiable();
        var handler = new GetPublicationByIdQueryHandler(publicationRepositoryMock.Object);

        var returnedPublication = await handler.Handle(query, default);

        publicationRepositoryMock.Verify(x => x.GetByIdAsync(It.Is<string>(x => x == id)), Times.Exactly(1));
        Assert.NotNull(returnedPublication);
        Assert.Equal(publication.Title, returnedPublication.Title);
    }
}