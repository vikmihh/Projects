using App.Contracts.DAL;
using App.DTO.Identity;
using Base.DAL.EF;
using Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class OrderRepository : BaseEntityRepository<App.DTO.Order, App.Domain.Order, AppDbContext>, IOrderRepository
{
    private readonly IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> _ticketInOrderMapper;
    private readonly IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> _itemInOrderMapper;
    private readonly IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> _userCouponMapper;
    private readonly IMapper<App.DTO.UserCategory, App.Domain.UserCategory> _userCategoryMapper;
    private readonly IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> _couponCategoryMapper;
    private readonly IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> _userInCategoryMapper;

    public OrderRepository(AppDbContext dbContext, IMapper<App.DTO.Order, App.Domain.Order> mapper,
        IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> ticketInOrderMapper,
        IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> itemInOrderMapper,
        IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> userCouponMapper,
        IMapper<App.DTO.UserCategory, App.Domain.UserCategory> userCategoryMapper,
        IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> couponCategoryMapper,
        IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> userInCategoryMapper
    ) : base(dbContext,
        mapper)
    {
        _ticketInOrderMapper = ticketInOrderMapper;
        _itemInOrderMapper = itemInOrderMapper;
        _userCouponMapper = userCouponMapper;
        _userCategoryMapper = userCategoryMapper;
        _couponCategoryMapper = couponCategoryMapper;
        _userInCategoryMapper = userInCategoryMapper;
    }

    public async Task<int> GetOrdersAmount(Guid appUserId) =>
        await CreateQuery().Where(a => a.AppUserId.Equals(appUserId)).CountAsync();

    public async Task<App.DTO.Order> ProceedOrderConfirmation(App.DTO.Order order, Guid userId)
    {
        var orderToUpdate = await FirstOrDefaultAsync(order.Id);


        order.OrderNr = orderToUpdate!.OrderNr;
        order.AppUserId = orderToUpdate.AppUserId;
        order.Price = await new TicketInOrderRepository(RepoDbContext, _ticketInOrderMapper, Mapper,
                _itemInOrderMapper,
                _userCategoryMapper, _userCouponMapper, _couponCategoryMapper, _userInCategoryMapper)
            .CalculateTicketsInOrderPrice(order.Id) + await new ItemInOrderRepository(RepoDbContext,
            _itemInOrderMapper,
            Mapper, _ticketInOrderMapper, _userCategoryMapper, _userCouponMapper, _couponCategoryMapper,
            _userInCategoryMapper).CalculateItemsInOrderPrice(order.Id);
        var userCoupon = await new UserCouponRepository(RepoDbContext, _userCouponMapper, _ticketInOrderMapper, _itemInOrderMapper,
            _userCategoryMapper, _couponCategoryMapper, _userInCategoryMapper, Mapper).GetUserCouponByOrderId(order.Id);
        order.Discount = userCoupon == null ? 0 : userCoupon.CouponCategory!.Discount;
        order.FinalPrice = order.Price *(userCoupon == null?  1:1-userCoupon.CouponCategory!.Discount/100);
        order.InProcess = false;
        
        var ordersAmount = await GetOrdersAmount(userId);
        await new UserCategoryRepository(RepoDbContext, _userCategoryMapper, _userInCategoryMapper)
            .SetUserCategoryByOrdersAmount(userId, ordersAmount);
        await new CouponCategoryRepository(RepoDbContext, _couponCategoryMapper, _userCouponMapper,
                _ticketInOrderMapper, _itemInOrderMapper, _userCategoryMapper, _couponCategoryMapper,
                _userInCategoryMapper, Mapper)
            .SetUserCouponsByOrdersAmount(userId, ordersAmount);
        var updated = Update(order, userId);

        await RepoDbContext.SaveChangesAsync();
        return updated;
    }

    public async Task<IEnumerable<App.DTO.Order>> GetAllOrdersByUserId(Guid userId)
    {
        return (await CreateQuery().Where(o => o.AppUserId.Equals(userId) && o.InProcess == false).Include("Card").ToListAsync()).Select(x => Mapper.Map(x)!);
    }
    public async Task<App.DTO.Order> GetCurrentOrderByUserIdAsync(Guid userId, bool noTracking = true)
    {
        var currentOrder = await CreateQuery(noTracking)
            .Include("ItemsInOrder")
            .Where(a => a.AppUserId.Equals(userId) && a.InProcess)
            .FirstOrDefaultAsync();
        if (currentOrder != null) return Mapper.Map(currentOrder)!;
        var newOrder = new App.DTO.Order
        {
            AppUserId = userId,
            InProcess = true,
            OrderNr = await GetOrdersAmount(userId) + 1
        };
        return Add(newOrder, userId);
    }
}