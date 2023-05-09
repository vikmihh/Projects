using App.DTO.Identity;
using Base.Domain;

namespace App.DTO;

public class UserInCategory : DomainEntityMetaId
{
    
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid UserCategoryId { get; set; }
    public UserCategory? UserCategory { get; set; }
}