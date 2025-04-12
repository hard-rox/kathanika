using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Schema.PatronGraph;

public sealed record CreatePatronPayload
    : Payload<Patron>
{
    public CreatePatronPayload(KnResult<Patron> knResult) : base(
        knResult,
        knResult.IsSuccess ? $"New patron {knResult.Value.FullName} added successfully." : "New patron add failed."
    )
    {
    }
}

public sealed record DeletePatronPayload
    : Payload
{
    public DeletePatronPayload(string id, KnResult knResult) : base(
        knResult,
        knResult.IsSuccess ? $"Patron with Id: {id} deleted." : $"Patron with Id: {id} deletion failed."
    )
    {
    }
}

public sealed record UpdatePatronPayload
    : Payload<Patron>
{
    public UpdatePatronPayload(KnResult<Patron> knResult) : base(knResult,
        $"Patron {knResult.Value.FullName} updated successfully.")
    {
    }
}