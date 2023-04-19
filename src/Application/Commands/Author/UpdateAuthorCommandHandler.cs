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
        var errors = new List<DomainException>();

        var existingAuthor = await authorRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundWithTheIdException(typeof(Author), request.Id);
        
        if (request.Patch.DateOfBirth?.ToUniversalTime().Date > DateTime.UtcNow.Date)
        {
            errors.Add(new InvalidFieldException(nameof(request.Patch.DateOfBirth), $"Cann't be future date"));
        }

        if (errors.Count > 0)
        {
            throw new AggregateException(errors);
        }

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