using Kathanika.Core.Domain.DomainEvents;
using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.PublisherAggregate;

public sealed class Publisher : AggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? ContactInformation { get; private set; }

    private Publisher(
        string publisherName,
        string? description,
        string? contactInformation
    )
    {
        Name = publisherName;
        Description = description;
        ContactInformation = contactInformation;
    }

    public static Result<Publisher> Create(
        string publisherName,
        string? description = null,
        string? contactInformation = null
    )
    {
        Publisher publisher = new(
            publisherName,
            description,
            contactInformation
        );
        return Result.Success(publisher);
    }
    public Result Update(
        string? publisherName = null,
        string? description = null,
        string? contactInformation = null
    )
    {
        Name = !string.IsNullOrWhiteSpace(publisherName) ? publisherName : Name;
        Description = !string.IsNullOrWhiteSpace(description) ? description : Description;
        ContactInformation = !string.IsNullOrWhiteSpace(contactInformation) ? contactInformation : ContactInformation;

        AddDomainEvent(new PublisherUpdatedDomainEvent(Id));

        return Result.Success();
    }
}
