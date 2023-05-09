using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserCouponMapper : BaseMapper<App.DTO.UserCoupon, Domain.UserCoupon>
{
    public UserCouponMapper(IMapper mapper) : base(mapper)
    {
    }
}