namespace Kathanika.Core.Application.Features.Patrons.Commands;
public sealed record DeletePatronCommand(string Id) : IRequest<Result>;
