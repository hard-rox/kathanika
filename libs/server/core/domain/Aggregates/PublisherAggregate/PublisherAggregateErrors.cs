using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.PublisherAggregate;

public static class PublisherAggregateErrors
{
    public static KnError NotFound(string id) => new(
        "Publisher.NotFound",
        message: $"No Publisher found with this Id: \"{id}\""
    );
}
