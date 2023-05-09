using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class UserInCategory : DomainEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public Guid UserCategoryId { get; set; }
    public UserCategory? UserCategory { get; set; }
    public string? UserCategoryName { get; set; }
}