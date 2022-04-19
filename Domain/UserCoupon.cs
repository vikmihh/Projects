using System.ComponentModel.DataAnnotations;
using Domain.Identity;

namespace Domain;

public class UserCoupon : BaseEntity
{
    [MinLength(1)]
    [MaxLength(256)]
    public int CouponNr { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(20)]
    public int PromoCode { get; set; } = default!;
    
    public bool IsUsed { get; set; } = default!;
    
    public decimal Discount { get; set; } = default!;
    
    public DateTime ValidFrom { get; set; } = default!;
    
    public DateTime ValidUntil { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}