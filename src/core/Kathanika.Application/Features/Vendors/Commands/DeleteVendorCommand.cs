namespace Kathanika.Application.Features.Vendors.Commands;

public sealed record DeleteVendorCommand(string Id) : IRequest<KnResult>;