namespace Kathanika.Core.Application.Features.Authors.Commands;

internal sealed class AddAuthorCommandHandler(IAuthorRepository authorRepository) : IRequestHandler<AddAuthorCommand, Result<Author>>
{
    public async Task<Result<Author>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        Result<Author> newAuthorResult = Author.Create(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.DateOfDeath,
            request.Nationality,
            request.Biography,
            request.DpFileId
        );

        if (newAuthorResult.IsFailure)
            return newAuthorResult;

        Author savedAuthor = await authorRepository.AddAsync(newAuthorResult.Value, cancellationToken);
        return Result.Success(savedAuthor);
    }
}
