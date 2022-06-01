using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IOrderRepository : IEntityRepository<App.DTO.Order>, IOrderRepositoryCustom<App.DTO.Order>
{
    
}

public interface IOrderRepositoryCustom<TEntity>
{
    // //return: empty object if unfinished order is not existing, unfinished order otherwise
    // Task<IEnumerable<TEntity>> GetUnfinishedOrderByUserId(string firstName, bool noTracking = true);
    // //params: MenuItemId, Amount
    // Task<IEnumerable<TEntity>> AddMenuItemByOrderId(string firstName, bool noTracking = true);
    // Task<IEnumerable<TEntity>> AddTicketByOrderId(string firstName, bool noTracking = true);

    Task<TEntity> ProceedOrderConfirmation(TEntity order, Guid userId);

    Task<int> GetOrdersAmount(Guid appUserId);

    Task<TEntity> GetCurrentOrderByUserIdAsync(Guid userId, bool noTracking = true);
    Task<IEnumerable<TEntity>> GetAllOrdersByUserId(Guid userId);
}