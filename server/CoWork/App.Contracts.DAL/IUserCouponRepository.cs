using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserCouponRepository : IEntityRepository<App.DTO.UserCoupon>, IUserCouponRepositoryCustom<App.DTO.UserCoupon>
{
    
}

public interface IUserCouponRepositoryCustom<TEntity>
{
    Task<TEntity> CreateCouponByUserId(Guid userId, Guid couponCategoryId, string couponCategoryName);
    Task<IEnumerable<TEntity>> GetAvailableUserCouponsByUserId(Guid userId);
    Task ActivateUserCouponByPromoCode(Guid userId, string promoCode, bool isAdding);
    Task<TEntity?> GetUserCouponByOrderId(Guid orderId,
        bool noTracking = true);

}