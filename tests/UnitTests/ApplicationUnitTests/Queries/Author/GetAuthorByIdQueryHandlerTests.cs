using Kathanika.Application.Queries;
using Xunit;

namespace Kathanika.UnitTests.ApplicationUnitTests.Queries;

public class GetAuthorByIdQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Return_Author_With_Specific_Id()
    {
        var author = Author.Create(
            "First Name",
            "Last Name",
            DateOnly.Parse("2000-01-01"),
            null,
            "",
            ""  
        );
        var id = Guid.NewGuid().ToString();
        var query = new GetAuthorByIdQuery(id);
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(author)
            .Verifiable();
        var handler = new GetAuthorByIdQueryHandler(authorRepositoryMock.Object);

        var returnedAuthor = await handler.Handle(query, default);

        authorRepositoryMock.Verify(x => x.GetByIdAsync(It.Is<string>(x => x == id), It.IsAny<CancellationToken>()), Times.Exactly(1));
        Assert.NotNull(returnedAuthor);
        Assert.Equal(author.FirstName, returnedAuthor.FirstName);
    }
}