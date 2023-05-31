namespace Kathanika.Application.Commands;

public sealed record MarkAuthorAsDeceasedCommand(
    string Id,
    DateOnly DateOfDeath
    ) : IRequest<Author>;
