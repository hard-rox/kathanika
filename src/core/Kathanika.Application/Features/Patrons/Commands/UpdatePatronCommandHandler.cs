using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Application.Features.Patrons.Commands;

internal sealed class UpdatePatronCommandHandler(
    IPatronRepository patronRepository
) : ICommandHandler<UpdatePatronCommand, KnResult<Patron>>
{
    public async Task<KnResult<Patron>> Handle(UpdatePatronCommand request, CancellationToken cancellationToken)
    {
        Patron? patron = await patronRepository.GetByIdAsync(request.Id, cancellationToken);
        if (patron is null)
            return PatronAggregateErrors.NotFound(request.Id);

        KnResult patronUpdateKnResult = patron.Update(
            request.Patch.CardNumber,
            request.Patch.Salutation,
            request.Patch.FirstName,
            request.Patch.Surname,
            request.Patch.PhotoFileId,
            request.Patch.DateOfBirth,
            request.Patch.Address,
            request.Patch.ContactNumber,
            request.Patch.Email
        );

        if (patronUpdateKnResult.IsFailure)
            return patronUpdateKnResult.Errors;

        await patronRepository.UpdateAsync(patron, cancellationToken);

        return patron;
    }
}