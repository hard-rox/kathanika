using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Vendors.Commands;

public sealed record AddVendorCommand(
    string Name,
    string Address,
    string ContactNumber,
    string? Email,
    string? Website,
    string? AccountDetail,
    string? ContactPersonName,
    string? ContactPersonPhone,
    string? ContactPersonEmail
) : IRequest<Result<Vendor>>;