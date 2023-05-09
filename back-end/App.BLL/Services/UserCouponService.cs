using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class UserCouponService : BaseEntityService<App.BLL.DTO.UserCoupon, App.DTO.UserCoupon, IUserCouponRepository>, IUserCouponService
{
    public UserCouponService(IUserCouponRepository repository, IMapper<BLL.DTO.UserCoupon, App.DTO.UserCoupon> mapper) : base(repository, mapper)
    {
    }


    public async Task<App.BLL.DTO.UserCoupon> CreateCouponByUserId(Guid userId, Guid couponCategoryId, string couponCategoryName)
    {
        return Mapper.Map(await Repository.CreateCouponByUserId(userId, couponCategoryId,couponCategoryName))!;
    }

    public async Task<IEnumerable<UserCoupon>> GetAvailableUserCouponsByUserId(Guid userId)
    {
        return (await Repository.GetAvailableUserCouponsByUserId(userId)).Select(x=>Mapper.Map(x)!);
    }

    public async Task ActivateUserCouponByPromoCode(Guid userId, string promoCode,bool isAdding)
    {
        await Repository.ActivateUserCouponByPromoCode(userId, promoCode,isAdding);
    }

    public async Task<App.BLL.DTO.UserCoupon?> GetUserCouponByOrderId(Guid orderId, bool noTracking = true)
    {
       return Mapper.Map(await Repository.GetUserCouponByOrderId(orderId));
    }
}