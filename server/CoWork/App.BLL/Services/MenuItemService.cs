using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class MenuItemService : BaseEntityService<App.BLL.DTO.MenuItem, App.DTO.MenuItem, IMenuItemRepository>, IMenuItemService
{
    public MenuItemService(IMenuItemRepository repository, IMapper<BLL.DTO.MenuItem, App.DTO.MenuItem> mapper) : base(repository, mapper)
    {
    }
    public async Task<IEnumerable<MenuItem>> GetAllByCategoryIdAsync(Guid id, bool noTracking = true)
    {
        return (await Repository.GetAllByCategoryIdAsync(id)).Select(x => Mapper.Map(x)!);
    }

    public async Task<IEnumerable<MenuItem>> GetAvailableMenuItems()
    {
        return (await Repository.GetAvailableMenuItems()).Select(x => Mapper.Map(x)!);
    }
}