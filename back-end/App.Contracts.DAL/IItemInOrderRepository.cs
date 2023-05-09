using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IItemInOrderRepository : IEntityRepository<App.DTO.ItemInOrder>, IItemInOrderRepositoryCustom<App.DTO.ItemInOrder>
{
  
}

public interface IItemInOrderRepositoryCustom<TEntity>
{
    Task<decimal> CalculateItemsInOrderPrice(Guid orderId);
    Task<TEntity> RemoveItemInOrderAsync(Guid itemInOrderId, Guid userId, int amount);
    Task<TEntity> AddItemInCurrentOrderAsync(Guid userId, Guid menuItemId, int amount);

    Task<IEnumerable<TEntity>> GetItemsInOrderByOrderId(Guid orderId, bool noTracking = true);
}