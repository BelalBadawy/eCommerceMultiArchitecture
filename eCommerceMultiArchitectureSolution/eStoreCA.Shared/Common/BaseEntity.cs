using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Shared.Common;

public abstract class BaseEntity<TId> : IEntity<TId>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    [NotMapped] public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    [Key] public TId Id { get; set; }

    public virtual List<IDomainEvent> PopDomainEvents()
    {
        var copy = _domainEvents.ToList();
        ClearDomainEvents();
        return copy;
    }

    public virtual void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public virtual void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public virtual void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}