using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class TicketInOrderService : BaseEntityService<App.BLL.DTO.TicketInOrder, App.DTO.TicketInOrder, ITicketInOrderRepository>, ITicketInOrderService
{
    public TicketInOrderService(ITicketInOrderRepository repository, IMapper<BLL.DTO.TicketInOrder, App.DTO.TicketInOrder> mapper) : base(repository, mapper)
    {
    }

    public async Task<decimal> CalculateTicketsInOrderPrice(Guid orderId)
    {
        return await Repository.CalculateTicketsInOrderPrice(orderId);
    }

    public async Task<IEnumerable<TicketInOrder>> GetTicketsInOrderByOrderId(Guid orderId, bool noTracking = true)
    {
        return (await Repository.GetTicketsInOrderByOrderId(orderId)).Select(x=>Mapper.Map(x)!);
    }

    public async Task<App.BLL.DTO.TicketInOrder> AddTicketInCurrentOrderAsync(Guid userId, Guid ticketId)
    {
        return Mapper.Map(await Repository.AddTicketInCurrentOrderAsync(userId, ticketId))!;
    }

    public async Task<IEnumerable<App.BLL.DTO.TicketInOrder>> GetAvailableTicketsByUserId(Guid userId, bool noTracking = true)
    {
        return (await Repository.GetAvailableTicketsByUserId(userId)).Select(x=>Mapper.Map(x)!);
    }

    public async Task<App.BLL.DTO.TicketInOrder> ActivateTicketByTicketInOrderId(Guid ticketInOrderId, bool noTracking = true)
    {
        return Mapper.Map(await Repository.ActivateTicketByTicketInOrderId(ticketInOrderId))!;
    }
}