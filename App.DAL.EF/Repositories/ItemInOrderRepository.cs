using App.Contracts.DAL;
using Base.DAL.EF;
using Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ItemInOrderRepository : BaseEntityRepository<App.DTO.ItemInOrder, App.Domain.ItemInOrder, AppDbContext>,
    IItemInOrderRepository
{
    private readonly IMapper<App.DTO.Order, App.Domain.Order> _orderMapper;
    private readonly IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> _ticketInOrderMapper;
    private readonly IMapper<App.DTO.UserCategory, App.Domain.UserCategory> _userCategoryMapper;
    private readonly IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> _userCouponMapper;
    private readonly IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> _couponCategoryMapper;
    private readonly IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> _userInCategoryMapper;


    public ItemInOrderRepository(AppDbContext dbContext, IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> mapper,
        IMapper<App.DTO.Order, App.Domain.Order> orderMapper,
        IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> ticketInOrderMapper,
        IMapper<App.DTO.UserCategory, App.Domain.UserCategory> userCategoryMapper,
        IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> userCouponMapper,
        IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> couponCategoryMapper,
        IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> userInCategoryMapper) : base(dbContext, mapper)
    {
        _ticketInOrderMapper = ticketInOrderMapper;
        _orderMapper = orderMapper;
        _userCategoryMapper = userCategoryMapper;
        _userCouponMapper = userCouponMapper;
        _couponCategoryMapper = couponCategoryMapper;
        _userInCategoryMapper = userInCategoryMapper;
    }
    
    public async Task<decimal> CalculateItemsInOrderPrice(Guid orderId)
    {
        return  (await CreateQuery().Include(x => x.MenuItem)
            .Where(i => i.OrderId.Equals(orderId) && i.DeletedAt == null).ToListAsync())
            .Select(x => x.Amount * x.MenuItem!.Price).AsEnumerable()
            .Aggregate(0m,(a, b) => a + b);

    }

    public async Task<IEnumerable<App.DTO.ItemInOrder>> GetItemsInOrderByOrderId(Guid orderId,
        bool noTracking = true)
    {
        return (await CreateQuery(noTracking)
                .Where(itemInOrder => itemInOrder.DeletedAt == null && itemInOrder.OrderId.CompareTo(orderId) == 0 )
                .Include("MenuItem").ToListAsync())
            .Select(x => Mapper.Map(x)!);
    }
    
    public async Task<App.DTO.ItemInOrder> RemoveItemInOrderAsync(Guid itemInOrderId, Guid userId, int amount)
    {
        var itemToRemove = await FirstOrDefaultAsync(itemInOrderId);
        if (itemToRemove == null)
        {
            throw new NullReferenceException($"Entity {nameof(App.DTO.TicketInOrder)} with id {itemInOrderId} was not found");
        }

        if (itemToRemove.Amount <= amount) return await RemoveAsync(itemInOrderId, userId);
        itemToRemove.Amount -= amount;
        return Update(itemToRemove, userId);
    }

    public async Task<App.DTO.ItemInOrder> AddItemInCurrentOrderAsync(Guid userId, Guid menuItemId, int amount)
    {
        var orderRepository = new OrderRepository(RepoDbContext,_orderMapper,_ticketInOrderMapper,
            Mapper,_userCouponMapper,_userCategoryMapper,_couponCategoryMapper,_userInCategoryMapper);
        var order = await orderRepository.GetCurrentOrderByUserIdAsync(userId);
        var existingItemInOrder = order.ItemsInOrder?.FirstOrDefault(i => i.MenuItemId.CompareTo(menuItemId) == 0);
        if (existingItemInOrder != null)
        {
            existingItemInOrder.Amount = existingItemInOrder.DeletedAt != null? amount: existingItemInOrder.Amount + amount;
            existingItemInOrder.DeletedAt = null;
            Update(existingItemInOrder,userId);
            return existingItemInOrder;
        }

        var itemInOrder = new App.DTO.ItemInOrder
        {
            MenuItemId = menuItemId,
            AppUserId = userId,
            OrderId = order.Id,
            Amount = amount
        };
        return Add(itemInOrder,userId);
    }
}