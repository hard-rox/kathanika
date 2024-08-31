using Kathanika.Core.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Core.Application.Features.Vendors.Commands;

public sealed record UpdateVendorCommand(string Id, VendorPatch Patch) : IRequest<Result<Vendor>>;

public sealed record VendorPatch(
        string? Name = null,
    string? Address = null,
    string? ContactNumber = null,
    string? Email = null,
    string? Website = null,
    string? AccountDetail = null,
    string? ContactPersonName = null,
    string? ContactPersonPhone = null,
    string? ContactPersonEmail = null,
    VendorStatus? Status = null
    );
