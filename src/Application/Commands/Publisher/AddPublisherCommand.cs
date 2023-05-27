namespace Kathanika.Application.Commands;

public sealed record AddPublisherCommand(
     string PublisherName,
     string Description,
     string ContactInformation
) : IRequest<Publisher>;