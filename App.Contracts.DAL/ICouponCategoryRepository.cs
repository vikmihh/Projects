using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICouponCategoryRepository : IEntityRepository<CouponCategory>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}