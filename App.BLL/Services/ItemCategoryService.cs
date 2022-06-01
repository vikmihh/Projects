using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;


public class ItemCategoryService : BaseEntityService<App.BLL.DTO.ItemCategory, App.DTO.ItemCategory, IItemCategoryRepository>, IItemCategoryService
{
    public ItemCategoryService(IItemCategoryRepository repository, IMapper<BLL.DTO.ItemCategory, App.DTO.ItemCategory> mapper) : base(repository, mapper)
    {
    }
    
}