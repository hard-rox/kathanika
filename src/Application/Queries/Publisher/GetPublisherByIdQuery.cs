namespace Kathanika.Application.Queries;

public sealed record GetPublisherByIdQuery(string Id) : IRequest<Publisher>;
