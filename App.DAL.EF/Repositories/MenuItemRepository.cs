using App.Contracts.DAL;
using Base.DAL.EF;
using Base.Contracts;
using Microsoft.EntityFrameworkCore;
using MenuItem = App.DTO.MenuItem;

namespace App.DAL.EF.Repositories;

public class MenuItemRepository : BaseEntityRepository<App.DTO.MenuItem, App.Domain.MenuItem, AppDbContext>, IMenuItemRepository
{
    public MenuItemRepository(AppDbContext dbContext, IMapper<App.DTO.MenuItem, App.Domain.MenuItem> mapper) : base(dbContext, mapper)
    {
    }
    

    public async Task<IEnumerable<App.DTO.MenuItem>> GetAllByCategoryIdAsync(Guid itemCategoryId, bool noTracking = true)
    {
        return (await CreateQuery(noTracking).Where(x=>x.ItemCategoryId.Equals(itemCategoryId) && x.DeletedAt ==null).Include("ItemCategory").ToListAsync())
            .Select(x => Mapper.Map(x)!);
    }

    public async Task<IEnumerable<App.DTO.MenuItem>> GetAvailableMenuItems()
    {
       return (await CreateQuery().Where(e => e.DeletedAt == null).Include("ItemCategory").ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}