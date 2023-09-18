namespace Kathanika.Application.Publishers.Commands;

public sealed record AddPublisherCommand(
     string PublisherName,
     string Description,
     string ContactInformation
) : IRequest<Publisher>;