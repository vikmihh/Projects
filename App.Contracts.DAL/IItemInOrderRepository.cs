using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IItemInOrderRepository : IEntityRepository<App.DTO.ItemInOrder>, IItemInOrderRepositoryCustom<App.DTO.ItemInOrder>
{
  
}

public interface IItemInOrderRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}