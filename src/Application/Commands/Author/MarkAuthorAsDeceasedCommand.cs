namespace Kathanika.Application.Commands;

public sealed record MarkAuthorAsDeceasedCommand(
    string Id,
    DateTime DateOfDeath
    ) : IRequest<Author>;
