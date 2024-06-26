using Kathanika.Core.Application.Features.Authors.Queries;

namespace Kathanika.Core.Application.Test.Queries;

public class GetAuthorByIdQueryHandlerTests
{
    [Fact]
    public async Task Handler_Should_Return_Author_With_Specific_Id()
    {
        Author author = Author.Create(
            "First Name",
            "Last Name",
            DateOnly.Parse("2000-01-01"),
            null,
            "",
            ""
        );
        string id = Guid.NewGuid().ToString();
        GetAuthorByIdQuery query = new(id);
        IAuthorRepository authorRepository = Substitute.For<IAuthorRepository>();
        authorRepository.GetByIdAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(author);
        GetAuthorByIdQueryHandler handler = new(authorRepository);

        Author? returnedAuthor = await handler.Handle(query, default);

        await authorRepository.Received(1).GetByIdAsync(Arg.Is<string>(x => x == id), Arg.Any<CancellationToken>());
        Assert.NotNull(returnedAuthor);
        Assert.Equal(author.FirstName, returnedAuthor.FirstName);
    }
}
