using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class CouponCategoryMapper : BaseMapper<App.DTO.CouponCategory, Domain.CouponCategory>
{
    public CouponCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}