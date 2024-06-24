namespace Kathanika.Core.Application.Features.Authors.Commands;

internal sealed class AddAuthorCommandHandler(IAuthorRepository authorRepository) : IRequestHandler<AddAuthorCommand, Author>
{
    public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        Author newAuthor = Author.Create(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.DateOfDeath,
            request.Nationality,
            request.Biography,
            request.DpFileId
        );

        Author savedAuthor = await authorRepository.AddAsync(newAuthor, cancellationToken);
        return savedAuthor;
    }
}
