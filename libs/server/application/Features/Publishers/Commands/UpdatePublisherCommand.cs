namespace Kathanika.Application.Features.Publishers.Commands;

public sealed record UpdatePublisherCommand(string Id, PublisherPatch Patch) : IRequest<Publisher>;

public sealed record PublisherPatch(string Name,
                                        string? Description,
                                        string? ContactInformation);
