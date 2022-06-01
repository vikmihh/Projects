using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ITicketInOrderRepository : IEntityRepository<App.DTO.TicketInOrder>,
    ITicketInOrderRepositoryCustom<App.DTO.TicketInOrder>
{
}

public interface ITicketInOrderRepositoryCustom<TEntity>
{

    Task<decimal> CalculateTicketsInOrderPrice(Guid orderId);
    Task<IEnumerable<TEntity>> GetTicketsInOrderByOrderId(Guid orderId,
        bool noTracking = true);

    Task<TEntity> AddTicketInCurrentOrderAsync(Guid userId, Guid ticketId);

    Task<IEnumerable<TEntity>> GetAvailableTicketsByUserId(Guid userId,
        bool noTracking = true);

    Task<TEntity> ActivateTicketByTicketInOrderId(Guid ticketInOrderId,
        bool noTracking = true);
}