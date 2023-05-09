using Base.Contracts.Domain;

namespace Base.Domain;

public abstract class DomainEntityMetaId : DomainEntityMetaId<Guid>, IDomainEntityId
{
    
}

public abstract class DomainEntityMetaId<TKey> : DomainEntityId<TKey>, IDomainEntityMeta<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey?  CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public TKey? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public TKey? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; } 
}