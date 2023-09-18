namespace Kathanika.Application.Authors.Commands;

public sealed record DeleteAuthorCommand(string Id) : IRequest;