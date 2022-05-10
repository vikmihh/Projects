using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class CouponCategoryService : BaseEntityService<App.BLL.DTO.CouponCategory, App.DTO.CouponCategory, ICouponCategoryRepository>, ICouponCategoryService
{
    public CouponCategoryService(ICouponCategoryRepository repository, IMapper<BLL.DTO.CouponCategory, App.DTO.CouponCategory> mapper) : base(repository, mapper)
    {
    }

    public Task<IEnumerable<CouponCategory>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}