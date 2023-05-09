using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserCouponMapper : BaseMapper<App.BLL.DTO.UserCoupon, App.DTO.UserCoupon>
{
    public UserCouponMapper(IMapper mapper) : base(mapper)
    {
    }
}