using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO.v1;

public class CouponCategory : DomainEntityMetaId
{
    [MinLength(1)]
    [MaxLength(128)]
    [DisplayName("Name")]
    public string Name { get; set; } = default!;
    public int OrdersAmount { get; set; } = default!;
    
   
    public decimal Discount { get; set; } = default!;
    
    public ICollection<UserCoupon>? UserCoupons { get; set; }
}