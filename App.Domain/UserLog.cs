using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserLog : DomainEntityMetaId
{
    public DateTime From { get; set; } = default!;
    
    public DateTime Until { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}