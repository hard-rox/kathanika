using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Patrons.Commands;
public sealed record DeletePatronCommand(string Id) : IRequest<Result>;