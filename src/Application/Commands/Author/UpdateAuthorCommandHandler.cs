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
        var existingAuthor = await authorRepository.GetByIdAsync(request.Id)
            ?? throw new Exception("No author found with this Id");
        
        existingAuthor.Update(
            request.Data.FirstName,
            request.Data.LastName,
            request.Data.DateOfBirth,
            request.Data.Nationality,
            request.Data.Biography
        );

        await authorRepository.UpdateAsync(existingAuthor);
        return existingAuthor;
    }
}