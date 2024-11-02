
namespace Kathanika.Core.Application.Features.Patrons;

internal sealed class CreatePatronCommandHandler(
    IPatronRepository patronRepository
) : IRequestHandler<CreatePatronCommand, Result<Patron>>
{
    public async Task<Result<Patron>> Handle(CreatePatronCommand request, CancellationToken cancellationToken)
    {
        Result<Patron> patronResult = Patron.Create(
            request.Surname,
            request.CardNumber,
            request.Salutation,
            request.FirstName,
            request.PhotoFileId,
            request.DateOfBirth,
            request.Address,
            request.ContactNumber,
            request.Email
        );

        if (patronResult.IsFailure)
            return patronResult;

        Patron patron = await patronRepository.AddAsync(patronResult.Value, cancellationToken);
        return Result.Success(patron);
    }
}
