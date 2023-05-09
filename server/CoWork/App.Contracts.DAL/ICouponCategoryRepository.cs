using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICouponCategoryRepository : IEntityRepository<App.DTO.CouponCategory>, ICouponCategoryRepositoryCustom<App.DTO.CouponCategory>
{
    
}

public interface ICouponCategoryRepositoryCustom<TEntity>
{

    Task SetUserCouponsByOrdersAmount(Guid userId, int ordersAmount);

}