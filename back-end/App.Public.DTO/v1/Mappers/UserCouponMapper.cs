using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserCouponMapper : BaseMapper<Public.DTO.v1.UserCoupon, App.BLL.DTO.UserCoupon>
{
    public UserCouponMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.UserCoupon? MapToBll(Public.DTO.v1.UserCoupon? userCoupon)
    {
        if (userCoupon == null) return null;
        var res = new App.BLL.DTO.UserCoupon()
        {
            Id = userCoupon.Id,
            PromoCode = userCoupon.PromoCode,
            IsUsed = userCoupon.IsUsed,
            AppUserId = userCoupon.AppUserId,
            CouponCategoryId = userCoupon.CouponCategoryId,
            
          
        };
        return res;
    }
    
    public static Public.DTO.v1.UserCoupon? MapToPublic(App.BLL.DTO.UserCoupon? userCoupon)
    {
        if (userCoupon == null) return null;
        var res = new Public.DTO.v1.UserCoupon()
        {
            Id = userCoupon.Id,
            PromoCode = userCoupon.PromoCode,
            IsUsed = userCoupon.IsUsed,
            AppUserId = userCoupon.AppUserId,
            CouponCategoryId = userCoupon.CouponCategoryId,
            CouponCategoryDiscount = userCoupon.CouponCategory == null? 0:userCoupon.CouponCategory!.Discount,
            CouponCategoryName =userCoupon.CouponCategory == null? "": userCoupon.CouponCategory!.Name
            
        };
        return res;
    }
}