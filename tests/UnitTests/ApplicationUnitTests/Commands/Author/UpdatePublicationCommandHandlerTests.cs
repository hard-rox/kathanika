using Kathanika.Application.Commands;
using Kathanika.Domain.Exceptions;

namespace Kathanika.UnitTests.ApplicationUnitTests.Commands;

public class UpdatePublicationCommandHandlerTests
{
    [Fact]
    public async Task Handler_Should_Throw_Exception_On_Invalid_PublicationId()
    {
        var publicationId = Guid.NewGuid().ToString();
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        var publicationRepositoryMock = new Mock<IPublicationRepository>();
        var command = new UpdatePublicationCommand(publicationId, new UpdatePublicationCommand.PublicationPatch(
            "", "", PublicationType.Book, "", null, null, null, "" ///TODO: Fix up update in domain...
        ));
        var handler = new UpdatePublicationCommandHandler(publicationRepositoryMock.Object, authorRepositoryMock.Object);

        var exception = await Assert.ThrowsAsync<NotFoundWithTheIdException>(async () => await handler.Handle(command, default));

        Assert.IsAssignableFrom<NotFoundWithTheIdException>(exception);
        Assert.Equal(publicationId, exception.Id);
    }

    [Fact]
    public async Task Handler_Should_Call_UpdateAsync_With_Updated_Author_DateOfDeath()
    {
        var authorId = Guid.NewGuid().ToString();
        var author = Author.Create("John",
            "Doe",
            DateTime.MinValue,
            null,
            "",
            "");
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(author).Verifiable();
        authorRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Author>())).Verifiable();
        var command = new UpdateAuthorCommand(authorId, new UpdateAuthorCommand.AuthorPatch(
            "Updated First Name",
            "Updated Last Name"
        ));
        var handler = new UpdateAuthorCommandHandler(authorRepositoryMock.Object);

        var updatedAuthor = await handler.Handle(command, default);

        Assert.NotNull(updatedAuthor);
        Assert.Equal("Updated First Name", updatedAuthor.FirstName);
        Assert.Equal("Updated Last Name", updatedAuthor.LastName);
        authorRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<string>()), Times.Exactly(1));
        authorRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Author>(x => x == author)), Times.Exactly(1));
    }
}