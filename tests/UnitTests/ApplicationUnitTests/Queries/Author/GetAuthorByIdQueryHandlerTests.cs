using Kathanika.Application.Queries;

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
        var authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(author);
        var handler = new GetAuthorByIdQueryHandler(authorRepository);

        var returnedAuthor = await handler.Handle(query, default);

        await authorRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
        Assert.NotNull(returnedAuthor);
        Assert.Equal(author.FirstName, returnedAuthor.FirstName);
    }
}