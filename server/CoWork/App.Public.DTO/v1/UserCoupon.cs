using App.Public.DTO.v1.Identity;
using Base.Domain;

namespace App.Public.DTO.v1;

public class UserCoupon : DomainEntityId
{
    
    
    public string PromoCode { get; set; } = default!;
    
    public bool IsUsed { get; set; } = default!;

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public string CouponCategoryName { get; set; } = default!;
    public decimal CouponCategoryDiscount { get; set; } = default!;
    public Guid CouponCategoryId { get; set; }
    public CouponCategory? CouponCategory { get; set; }
    
    public Guid? OrderId { get; set; }
    public Order? Order { get; set; }
}