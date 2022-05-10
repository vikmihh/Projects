using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserCouponService : IEntityService<App.BLL.DTO.UserCoupon>, IUserCouponRepositoryCustom<App.BLL.DTO.UserCoupon>
{
    
}