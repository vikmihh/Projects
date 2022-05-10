using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IMenuItemService : IEntityService<App.BLL.DTO.MenuItem>, IMenuItemRepositoryCustom<App.BLL.DTO.MenuItem>
{
    
}