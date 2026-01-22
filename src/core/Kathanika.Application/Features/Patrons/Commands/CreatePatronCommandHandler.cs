using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Application.Features.Patrons.Commands;

internal sealed class CreatePatronCommandHandler(
    IPatronRepository patronRepository
) : ICommandHandler<CreatePatronCommand, KnResult<Patron>>
{
    public async Task<KnResult<Patron>> Handle(CreatePatronCommand request, CancellationToken cancellationToken)
    {
        KnResult<Patron> patronResult = Patron.Create(
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
        return patron;
    }
}