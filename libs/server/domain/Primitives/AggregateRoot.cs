namespace Kathanika.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    private List<IDomainEvent> domainEvents = new();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        domainEvents ??= new();
        domainEvents.Add(domainEvent);
    }

    public List<IDomainEvent> GetDomainEvents()
    {
        return domainEvents ?? new();
    }

    public void ClearDomainEvents()
    {
        domainEvents ??= new();
        domainEvents.Clear();
    }
}
