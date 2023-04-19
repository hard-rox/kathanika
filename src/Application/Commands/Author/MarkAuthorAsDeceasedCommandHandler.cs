using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Commands;

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

        if (request.DateOfDeath.ToUniversalTime().Date > DateTime.UtcNow.Date)
        {
            throw new InvalidFieldException(nameof(request.DateOfDeath), $"Cann't be future date");
        }

        existingAuthor.MakeAsDeceased(request.DateOfDeath);

        await authorRepository.UpdateAsync(existingAuthor);
        return existingAuthor;
    }
}
