using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IItemCategoryService : IEntityService<App.BLL.DTO.ItemCategory>, IItemCategoryRepositoryCustom<App.BLL.DTO.ItemCategory>
{
    
}