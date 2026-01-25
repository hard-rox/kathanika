using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Application.Features.Patrons.Commands;

public sealed record CreatePatronCommand(
    string Surname,
    string CardNumber,
    string? Salutation,
    string? FirstName,
    string? PhotoFileId,
    DateOnly? DateOfBirth,
    string? Address,
    string? ContactNumber,
    string? Email
) : ICommand<KnResult<Patron>>;