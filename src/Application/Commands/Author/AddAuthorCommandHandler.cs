using Kathanika.Application.Abstractions;

namespace Kathanika.Application.Commands;

internal sealed class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorRepository _authorRepository;

    public AddAuthorCommandHandler(IUnitOfWork unitOfWork, IAuthorRepository authorRepository)
    {
        _unitOfWork = unitOfWork;
        _authorRepository = authorRepository;
    }


    public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var newAuthor = new Author(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.DateOfDeath,
            request.Nationality,
            request.Biography
        );

        var savedAuthor = await _authorRepository.AddAsync(newAuthor);
        await _unitOfWork.CommitChangesAsync();
        return savedAuthor;
    }
}