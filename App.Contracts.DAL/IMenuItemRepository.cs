using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IMenuItemRepository : IEntityRepository<App.DTO.MenuItem>, IMenuItemRepositoryCustom<App.DTO.MenuItem>
{
    
}

public interface IMenuItemRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}