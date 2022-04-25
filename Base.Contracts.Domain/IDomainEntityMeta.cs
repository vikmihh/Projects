namespace Base.Contracts.Domain;

public interface IDomainEntityMeta
{
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string? UpdatedAt { get; set; }
    public DateTime UpdatedBy { get; set; }
}