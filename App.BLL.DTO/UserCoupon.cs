using App.BLL.DTO.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class UserCoupon : DomainEntityId
{
    public int CouponNr { get; set; } = default!;
    
    public int PromoCode { get; set; } = default!;
    
    public bool IsUsed { get; set; } = default!;
    
    public decimal Discount { get; set; } = default!;
    
    public DateTime ValidFrom { get; set; } = default!;
    
    public DateTime ValidUntil { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}