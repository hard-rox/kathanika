using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

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

    public static Publisher Create(
        string publisherName,
        string? description,
        string? contactInformation
    )
    {
        Publisher publisher = new(
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
        Name = !string.IsNullOrEmpty(publisherName) ? publisherName : Name;
        Description = !string.IsNullOrEmpty(description) ? description : Description;
        ContactInformation = !string.IsNullOrEmpty(contactInformation) ? contactInformation : ContactInformation;
    }
}
