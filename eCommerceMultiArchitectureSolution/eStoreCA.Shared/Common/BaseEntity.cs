using eStoreCA.Shared.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eStoreCA.Shared.Common
{

    public abstract class BaseEntity<TId> : IEntity<TId>
    {

        [Key]
        public TId Id { get; set; }

        private readonly List<IDomainEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public List<IDomainEvent> PopDomainEvents()
        {
            var copy = _domainEvents.ToList();

            ClearDomainEvents();

            return copy;
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }



        #region Custom
        #endregion Custom


    }
}
