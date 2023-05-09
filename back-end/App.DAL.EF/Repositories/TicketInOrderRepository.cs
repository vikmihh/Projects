using App.Contracts.DAL;
using Base.DAL.EF;
using Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TicketInOrderRepository :
    BaseEntityRepository<App.DTO.TicketInOrder, App.Domain.TicketInOrder, AppDbContext>, ITicketInOrderRepository
{
    private readonly IMapper<App.DTO.Order, App.Domain.Order> _orderMapper;
    private readonly IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> _itemInOrderMapper;
    private readonly IMapper<App.DTO.UserCategory, App.Domain.UserCategory> _userCategoryMapper;
    private readonly IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> _userCouponMapper;
    private readonly IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> _couponCategoryMapper;
    private readonly IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> _userInCategoryMapper;


    public TicketInOrderRepository(AppDbContext dbContext,
        IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> mapper,
        IMapper<App.DTO.Order, App.Domain.Order> orderMapper,
        IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> itemInOrderMapper,
        IMapper<App.DTO.UserCategory, App.Domain.UserCategory> userCategoryMapper,
        IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> userCouponMapper,
        IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> couponCategoryMapper,
        IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> userInCategoryMapper
    ) : base(dbContext, mapper)
    {
        _orderMapper = orderMapper;
        _itemInOrderMapper = itemInOrderMapper;
        _userCategoryMapper = userCategoryMapper;
        _userCouponMapper = userCouponMapper;
        _couponCategoryMapper = couponCategoryMapper;
        _userInCategoryMapper = userInCategoryMapper;
    }

    public async Task<decimal> CalculateTicketsInOrderPrice(Guid orderId)
    {
        return (await CreateQuery().Include(x => x.Ticket)
                .Where(i => i.OrderId.Equals(orderId) && i.DeletedAt == null).ToListAsync())
            .Select(x => x.Ticket!.Price).AsEnumerable()
            .Aggregate(0m, (a, b) => a + b);
    }

    public async Task<App.DTO.TicketInOrder> AddTicketInCurrentOrderAsync(Guid userId, Guid ticketId)
    {
        var order = await new OrderRepository(RepoDbContext, _orderMapper)
            .GetCurrentOrderByUserIdAsync(userId);

        var ticketInOrder = Add(new App.DTO.TicketInOrder
        {
            TicketId = ticketId,
            AppUserId = userId,
            OrderId = order.Id
        }, userId);
        return ticketInOrder;
    }

    public async Task<IEnumerable<App.DTO.TicketInOrder>> GetAvailableTicketsByUserId(Guid userId,
        bool noTracking = true)
    {
        return (await CreateQuery(noTracking).Include("Order").Include("Ticket")
                .Where(ticketInOrder => ticketInOrder.DeletedAt == null &&
                                        ticketInOrder.AppUserId.CompareTo(userId) == 0 &&
                                        !ticketInOrder.Order!.InProcess &&
                                        (!ticketInOrder.Activated ||
                                         ticketInOrder.ValidUntil!.Value.CompareTo(DateTime.UtcNow) > 0)).ToListAsync())
            .Select(x => Mapper.Map(x)!);
    }


    public async Task<IEnumerable<App.DTO.TicketInOrder>> GetTicketsInOrderByOrderId(Guid orderId,
        bool noTracking = true)
    {
        return (await CreateQuery(noTracking)
                .Where(ticketInOrder => ticketInOrder.DeletedAt == null &&
                                        ticketInOrder.OrderId.CompareTo(orderId) == 0)
                .Include("Ticket").ToListAsync())
            .Select(x => Mapper.Map(x)!);
    }


    public async Task<bool> IsTicketValid(Guid ticketInOrderId, bool noTracking = true) =>
        await CreateQuery(noTracking)
            .Where(ticketInOrder => ticketInOrder.Id.Equals(ticketInOrderId) && ticketInOrder.Activated
                                                                             && ticketInOrder.ValidUntil!.Value
                                                                                 .CompareTo(DateTime.UtcNow) > 0)
            .AnyAsync();


    public async Task<App.DTO.TicketInOrder> ActivateTicketByTicketInOrderId(Guid ticketInOrderId,
        bool noTracking = true)
    {
        var ticketToActivate = await CreateQuery(noTracking)
            .Where(ticketInOrder => ticketInOrder.Id.CompareTo(ticketInOrderId) == 0).Include("Ticket")
            .FirstOrDefaultAsync();
        ticketToActivate!.Activated = true;
        ticketToActivate.ValidFrom = DateTime.UtcNow;
        ticketToActivate.ValidUntil = DateTime.UtcNow.AddDays(ticketToActivate.Ticket!.DayRange);

        return Mapper.Map(ticketToActivate)!;
    }
}