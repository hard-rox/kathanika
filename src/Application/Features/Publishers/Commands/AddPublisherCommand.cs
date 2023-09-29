namespace Kathanika.Application.Features.Publishers.Commands;

public sealed record AddPublisherCommand(
     string Name,
     string? Description,
     string? ContactInformation
) : IRequest<Publisher>;
