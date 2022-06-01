using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IMenuItemRepository : IEntityRepository<App.DTO.MenuItem>, IMenuItemRepositoryCustom<App.DTO.MenuItem>
{
    
}

public interface IMenuItemRepositoryCustom<TEntity>
{
    // //param: categoryId, return: all possible MenuItems by categoryId
    // Task<IEnumerable<TEntity>> GetAllByCategoryId(string firstName, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllByCategoryIdAsync(Guid id, bool noTracking = true);
    
    Task<IEnumerable<TEntity>> GetAvailableMenuItems();
}