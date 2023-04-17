using Kathanika.Domain.Premitives;

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
        var existingAuthor = await authorRepository.GetByIdAsync(request.Id);
        if(existingAuthor is null)
            throw new Exception("No author found with this Id");

        existingAuthor.Update(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Nationality,
            request.Biography
        );

        await authorRepository.UpdateAsync(existingAuthor);
        return existingAuthor;
    }
}