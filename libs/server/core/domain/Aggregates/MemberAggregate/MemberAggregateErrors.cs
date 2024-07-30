using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.MemberAggregate;

public static class MemberAggregateErrors
{
    public static KnError HasIssuedPublication(int issuedPublicationLength) => new(
        "Member.HasIssuedPublication",
        message: $"Member has issued {issuedPublicationLength} publications."
    );

    public static KnError NotFound(string id) => new(
        "Member.NotFound",
        message: $"No Member found with this Id: \"{id}\""
    );

    public static readonly KnError CancelledMembership = new(
        "Member.CancelledMembership",
        message: "Membership is cancelled"
    );

    public static readonly KnError ActiveMembership = new(
        "Member.CancelledMembership",
        message: "Membership is active"
    );
}
