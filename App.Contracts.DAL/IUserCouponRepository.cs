using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserCouponRepository : IEntityRepository<UserCoupon>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}