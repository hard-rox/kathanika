namespace Kathanika.Application.Authors.Commands;

public sealed record MarkAuthorAsDeceasedCommand(
    string Id,
    DateOnly DateOfDeath
    ) : IRequest<Author>;
