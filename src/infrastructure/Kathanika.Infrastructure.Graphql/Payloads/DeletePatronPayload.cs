using Kathanika.Infrastructure.Graphql.Bases;

namespace Kathanika.Infrastructure.Graphql.Payloads;
public sealed record DeletePatronPayload
    : Payload
{
    public DeletePatronPayload(string id, Result result) : base(
      result,
      result.IsSuccess ?
      $"Patron with Id: {id} deleted." :
      $"Patron with Id: {id} deletion failed."
  )
    {
    }
}
