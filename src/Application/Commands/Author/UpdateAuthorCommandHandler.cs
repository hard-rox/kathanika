using Kathanika.Domain.Exceptions;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Commands;

internal sealed class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
{
    private readonly IAuthorRepository authorRepository;

    public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await authorRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundWithTheIdException(typeof(Author), request.Id);
        
        existingAuthor.Update(
            request.Patch.FirstName,
            request.Patch.LastName,
            request.Patch.DateOfBirth,
            request.Patch.Nationality,
            request.Patch.Biography
        );

        await authorRepository.UpdateAsync(existingAuthor);
        return existingAuthor;
    }
}