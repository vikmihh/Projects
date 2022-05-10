using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class CouponCategoryRepository : BaseEntityRepository<App.DTO.CouponCategory, App.Domain.CouponCategory, AppDbContext>, ICouponCategoryRepository
{
    public CouponCategoryRepository(AppDbContext dbContext, IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.CouponCategory>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}