namespace Kathanika.Core.Application.Features.Vendors.Commands;

public sealed record DeleteVendorCommand(string Id) : IRequest<Result>;
