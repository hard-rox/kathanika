using Kathanika.Domain.Aggregates.BibItemAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.BibItemGraph;

public sealed record AddBibItemPayload
    : Payload<BibItem>
{
    public AddBibItemPayload(KnResult<BibItem> knResult) : base(
        knResult,
        knResult.IsSuccess
            ? $"New BibItem with barcode {knResult.Value.Barcode} added successfully."
            : "New BibItem add failed."
    )
    {
    }
}

public sealed record UpdateBibItemPayload
    : Payload<BibItem>
{
    public UpdateBibItemPayload(KnResult<BibItem> knResult)
        : base(knResult,
            knResult.IsSuccess
                ? $"BibItem with barcode {knResult.Value.Barcode} updated successfully."
                : "BibItem update failed.")
    {
    }
}

public sealed record CheckOutBibItemPayload
    : Payload<BibItem>
{
    public CheckOutBibItemPayload(KnResult<BibItem> knResult)
        : base(knResult,
            knResult.IsSuccess
                ? $"BibItem with barcode {knResult.Value.Barcode} checked out successfully."
                : "BibItem check-out failed.")
    {
    }
}

public sealed record CheckInBibItemPayload
    : Payload<BibItem>
{
    public CheckInBibItemPayload(KnResult<BibItem> knResult)
        : base(knResult,
            knResult.IsSuccess
                ? $"BibItem with barcode {knResult.Value.Barcode} checked in successfully."
                : "BibItem check-in failed.")
    {
    }
}

public sealed record WithdrawBibItemPayload
    : Payload<BibItem>
{
    public WithdrawBibItemPayload(KnResult<BibItem> knResult)
        : base(knResult,
            knResult.IsSuccess
                ? $"BibItem with barcode {knResult.Value.Barcode} withdrawn successfully."
                : "BibItem withdrawal failed.")
    {
    }
}