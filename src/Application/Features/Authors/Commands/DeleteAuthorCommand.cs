namespace Kathanika.Application.Features.Authors.Commands;

public sealed record DeleteAuthorCommand(string Id) : IRequest;