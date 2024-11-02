using Kathanika.Core.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Core.Application.Features.Vendors.Commands;

public sealed record AddVendorCommand(
    string Name,
    string Address,
    string ContactNumber,
    string? Email,
    string? Website,
    string? AccountDetail,
    string? ContactPersonName,
    string? ContactPersonPhone,
    string? ContactPersonEmail,
    VendorStatus Status
) : IRequest<Result<Vendor>>;
