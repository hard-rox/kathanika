namespace Kathanika.Application.Commands;

public sealed record DeleteAuthorCommand(string Id) : IRequest;