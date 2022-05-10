using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IItemCategoryRepository : IEntityRepository<App.DTO.ItemCategory>, IItemCategoryRepositoryCustom<App.DTO.ItemCategory>
{
   
}

public interface IItemCategoryRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}