namespace Kathanika.Core.Application.Features.Publishers.Commands;

public sealed record AddPublisherCommand(string Name,
                                         string? Description,
                                         string? ContactInformation) : IRequest<Result<Publisher>>;
