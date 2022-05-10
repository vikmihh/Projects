using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class MenuItemRepository : BaseEntityRepository<App.DTO.MenuItem, App.Domain.MenuItem, AppDbContext>, IMenuItemRepository
{
    public MenuItemRepository(AppDbContext dbContext, IMapper<App.DTO.MenuItem, App.Domain.MenuItem> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.MenuItem>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}