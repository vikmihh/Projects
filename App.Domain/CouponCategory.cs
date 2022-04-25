using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class CouponCategory : DomainEntityMetaId
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