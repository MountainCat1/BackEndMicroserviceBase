namespace BaseApp.Domain.Abstractions;


public interface IEntity
{
    IReadOnlyCollection<IDomainEvent>? DomainEvents { get; }
    void AddDomainEvent<T>(DomainEvent<T> notification) where T : Entity;
    void ClearDomainEvents();
}

public abstract class Entity : IEntity
{
    private List<IDomainEvent>? _domainEvents;
    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly()!;
    
    public void AddDomainEvent<T>(DomainEvent<T> notification) where T : Entity
    {
        notification.Entity = this as T ?? throw new NullReferenceException();
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(notification);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}