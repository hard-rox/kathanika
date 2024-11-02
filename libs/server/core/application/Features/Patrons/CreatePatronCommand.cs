namespace Kathanika.Core.Application.Features.Patrons;

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
) : IRequest<Result<Patron>>;
