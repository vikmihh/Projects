using App.DTO.Identity;
using Base.Domain;

namespace App.DTO;

public class UserLog : DomainEntityId
{
    public DateTime From { get; set; } = default!;
    
    public DateTime Until { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}