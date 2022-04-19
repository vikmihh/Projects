using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class CouponCategory : BaseEntity
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Name")]
    public string Name { get; set; } = default!;
    
    [MaxLength(256)]
    [DisplayName("Description")]
    public string Description { get; set; } = default!;
    
    public ICollection<UserCoupon>? UserCoupons { get; set; }
}