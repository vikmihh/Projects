using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class UserInCategory : DomainEntityId
{
    public DateTime From { get; set; } = default!;
    
    public DateTime Until { get; set; } = default!;
    
    public ICollection<UserCoupon>? UserCoupons { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}