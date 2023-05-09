using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class CouponCategoryMapper : BaseMapper<Public.DTO.v1.CouponCategory, App.BLL.DTO.CouponCategory>
{
    public CouponCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.CouponCategory? MapToBll(Public.DTO.v1.CouponCategory? couponCategory)
    {
        if (couponCategory == null) return null;
        var res = new App.BLL.DTO.CouponCategory()
        {
            Id = couponCategory.Id,
            Name = couponCategory.Name,
            Discount = couponCategory.Discount,
            OrdersAmount = couponCategory.OrdersAmount
        };
        return res;
    }
    
    public static Public.DTO.v1.CouponCategory? MapToPublic(App.BLL.DTO.CouponCategory? couponCategory)
    {
        if (couponCategory == null) return null;
        var res = new Public.DTO.v1.CouponCategory()
        {
            Id = couponCategory.Id,
            Name = couponCategory.Name,
            Discount = couponCategory.Discount,
            OrdersAmount = couponCategory.OrdersAmount
        };
        return res;
    }
}