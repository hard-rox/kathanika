using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class Publisher : AggregateRoot
{
    public string PublisherName { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string ContactInformation { get; private set; } = string.Empty;

    private Publisher(
        string publisherName,
        string description,
        string contactInformation
    )
    {
        PublisherName = publisherName;
        Description = description;
        ContactInformation = contactInformation;
    }

    public static Publisher Create(
        string publisherName,
        string description,
        string contactInformation
    )
    {
        var publisher = new Publisher(
            publisherName,
            description,
            contactInformation
        );
        return publisher;
    }
    public void Update(
        string? publisherName = null,
        string? description = null,
        string? contactInformation = null
    )
    {
        PublisherName = !string.IsNullOrEmpty(publisherName) ? publisherName :PublisherName;
        Description = !string.IsNullOrEmpty(description) ? description : Description;
        ContactInformation = !string.IsNullOrEmpty(contactInformation) ? contactInformation : ContactInformation;
    }
}
