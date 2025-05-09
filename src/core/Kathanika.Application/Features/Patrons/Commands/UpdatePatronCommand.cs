using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Application.Features.Patrons.Commands;

public sealed record UpdatePatronCommand(string Id, PatronPatch Patch) : IRequest<KnResult<Patron>>;

public sealed record PatronPatch(
    string? CardNumber,
    string? Salutation,
    string? FirstName,
    string? Surname,
    string? PhotoFileId,
    DateOnly? DateOfBirth,
    string? Address,
    string? ContactNumber,
    string? Email
);