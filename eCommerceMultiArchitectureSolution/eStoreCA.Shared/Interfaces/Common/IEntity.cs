namespace eStoreCA.Shared.Interfaces;

public interface IEntity<TId> : IEntity
{
    TId Id { get; set; }
}

public interface IEntity
{
}