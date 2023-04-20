using Kathanika.Domain.Exceptions;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Commands;

internal sealed class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
{
    private readonly IAuthorRepository authorRepository;

    public AddAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var errors = new List<DomainException>();
        if (request.DateOfBirth.ToUniversalTime().Date > DateTime.UtcNow.Date)
        {
            errors.Add(new InvalidFieldException(nameof(request.DateOfBirth), $"Cann't be future date"));
        }
        if (request.DateOfDeath?.ToUniversalTime().Date > DateTime.UtcNow.Date)
        {
            errors.Add(new InvalidFieldException(nameof(request.DateOfDeath), $"Cann't be future date"));
        }
        if(request.DateOfDeath is not null && request.DateOfDeath?.Date <= request.DateOfBirth)
        {
            errors.Add(new InvalidFieldException(nameof(request.DateOfDeath), $"DateOfDeath must be after DateOfDeath"));
        }

        if (errors.Count > 0)
        {
            throw new AggregateException(errors);
        }

        var newAuthor = new Author(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.DateOfDeath,
            request.Nationality,
            request.Biography
        );

        var savedAuthor = await authorRepository.AddAsync(newAuthor);
        return savedAuthor;
    }
}