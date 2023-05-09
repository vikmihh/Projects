using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class CouponCategoryMapper : BaseMapper<App.BLL.DTO.CouponCategory, App.DTO.CouponCategory>
{
    public CouponCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}