using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Patrons.Commands;
internal sealed class UpdatePatronCommandHandler(
    IPatronRepository patronRepository
    ) : IRequestHandler<UpdatePatronCommand, Result<Patron>>
{
    public async Task<Result<Patron>> Handle(UpdatePatronCommand request, CancellationToken cancellationToken)
    {
        Patron? patron = await patronRepository.GetByIdAsync(request.Id, cancellationToken);
        if (patron is null)
            return Result.Failure<Patron>(PatronAggregateErrors.NotFound(request.Id));

        Result patronUpdateResult = patron.Update(
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

        if (patronUpdateResult.IsFailure)
            return Result.Failure<Patron>(patronUpdateResult.Errors);

        await patronRepository.UpdateAsync(patron, cancellationToken);

        return Result.Success(patron);
    }
}