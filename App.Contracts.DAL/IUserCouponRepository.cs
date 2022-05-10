using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserCouponRepository : IEntityRepository<App.DTO.UserCoupon>, IUserCouponRepositoryCustom<App.DTO.UserCoupon>
{
    
}

public interface IUserCouponRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}