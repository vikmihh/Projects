using Domain.Identity;

namespace Domain;

public class UserLog : BaseEntity
{
    public DateTime From { get; set; } = default!;
    
    public DateTime Until { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}