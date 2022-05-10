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

    public Task<IEnumerable<MenuItem>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}