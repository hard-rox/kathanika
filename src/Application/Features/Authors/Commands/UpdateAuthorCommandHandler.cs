using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Authors.Commands;

internal sealed class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
{
    private readonly IAuthorRepository authorRepository;

    public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        Author existingAuthor = await authorRepository.GetByIdAsync(request.Id, cancellationToken) ??
            throw new NotFoundWithTheIdException(typeof(Author), request.Id);

        existingAuthor.Update(
            request.Patch.FirstName,
            request.Patch.LastName,
            request.Patch.DateOfBirth,
            request.Patch.Nationality,
            request.Patch.Biography
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
        return existingAuthor;
    }
}
