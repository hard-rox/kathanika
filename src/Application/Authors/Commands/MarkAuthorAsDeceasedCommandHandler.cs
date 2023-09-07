using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Authors.Commands;

internal sealed class MarkAuthorAsDeceasedCommandHandler
    : IRequestHandler<MarkAuthorAsDeceasedCommand, Author>
{
    private readonly IAuthorRepository authorRepository;

    public MarkAuthorAsDeceasedCommandHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<Author> Handle(MarkAuthorAsDeceasedCommand request, CancellationToken cancellationToken)
    {
        var existingAuthor = await authorRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundWithTheIdException(typeof(Author), request.Id);

        existingAuthor.MarkAsDeceased(request.DateOfDeath);

        await authorRepository.UpdateAsync(existingAuthor);
        return existingAuthor;
    }
}
