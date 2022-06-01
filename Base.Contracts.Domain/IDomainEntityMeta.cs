namespace Base.Contracts.Domain;

public interface IDomainEntityMeta : IDomainEntityMeta<Guid>
{
    
}
public interface IDomainEntityMeta <TKey>
where TKey: IEquatable<TKey>
{
    public TKey? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public TKey? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public TKey? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}