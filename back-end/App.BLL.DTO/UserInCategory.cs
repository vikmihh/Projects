using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class UserInCategory : DomainEntityId
{
    
  
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid UserCategoryId { get; set; }
    public UserCategory? UserCategory { get; set; }
}