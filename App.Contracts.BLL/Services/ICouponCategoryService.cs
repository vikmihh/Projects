using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICouponCategoryService : IEntityService<App.BLL.DTO.CouponCategory>, ICouponCategoryRepositoryCustom<App.BLL.DTO.CouponCategory>
{
    
}