using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class MenuItemRepository : BaseEntityRepository<MenuItem, AppDbContext>, IMenuItemRepository
{
    public MenuItemRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}