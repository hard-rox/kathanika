namespace Kathanika.Core.Application.Features.Authors.Commands;

internal sealed class UpdateAuthorCommandHandler(IAuthorRepository authorRepository) : IRequestHandler<UpdateAuthorCommand, Result<Author>>
{
    public async Task<Result<Author>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        Author? existingAuthor = await authorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existingAuthor is null)
            return Result.Failure<Author>(AuthorAggregateErrors.NotFoundError(request.Id));

        existingAuthor.Update(
            request.Patch.FirstName,
            request.Patch.LastName,
            request.Patch.DateOfBirth,
            request.Patch.Nationality,
            request.Patch.Biography,
            request.Patch.DpFileId
        );

        if (request.Patch.MarkedAsDeceased)
        {
            existingAuthor.MarkAsDeceased(request.Patch.DateOfDeath ?? new DateOnly());
        }
        else
        {
            existingAuthor.UnmarkAsDeceased();
        }

        await authorRepository.UpdateAsync(existingAuthor, cancellationToken);
        return Result.Success(existingAuthor);
    }
}
