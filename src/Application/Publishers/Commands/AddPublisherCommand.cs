namespace Kathanika.Application.Publishers.Commands;

public sealed record AddPublisherCommand(
     string Name,
     string? Description,
     string? ContactInformation
) : IRequest<Publisher>;