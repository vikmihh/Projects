using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class OrderService : BaseEntityService<App.BLL.DTO.Order, App.DTO.Order, IOrderRepository>, IOrderService
{
    private IMapper<BLL.DTO.TicketInOrder, App.DTO.TicketInOrder> TicketInOrderMapper { get; set; }
    private ITicketInOrderRepository TicketInOrderRepository { get; set; }
    private IMapper<BLL.DTO.ItemInOrder, App.DTO.ItemInOrder> ItemInOrderMapper { get; set; }
    private IItemInOrderRepository ItemInOrderRepository { get; set; }
    private IMapper<BLL.DTO.UserCoupon, App.DTO.UserCoupon> UserCouponMapper { get; set; }
    private IUserCouponRepository UserCouponRepository { get; set; }
    private IMapper<BLL.DTO.UserCategory, App.DTO.UserCategory> UserCategoryMapper { get; set; }
    private IUserCategoryRepository UserCategoryRepository { get; set; }
    private IMapper<BLL.DTO.CouponCategory, App.DTO.CouponCategory> CouponCategoryMapper { get; set; }
    private ICouponCategoryRepository CouponCategoryRepository { get; set; }
    public OrderService(IOrderRepository repository, IMapper<BLL.DTO.Order, App.DTO.Order> mapper,
        ITicketInOrderRepository ticketInOrderRepository,
        IMapper<BLL.DTO.TicketInOrder, App.DTO.TicketInOrder> ticketInOrderMapper,
        IItemInOrderRepository itemInOrderRepository,
        IMapper<BLL.DTO.ItemInOrder, App.DTO.ItemInOrder> itemInOrderMapper,
        IUserCouponRepository userCouponRepository,
        IMapper<BLL.DTO.UserCoupon, App.DTO.UserCoupon> userCouponMapper,
        IUserCategoryRepository userCategoryRepository,
        IMapper<BLL.DTO.UserCategory, App.DTO.UserCategory> userCategoryMapper,
        ICouponCategoryRepository couponCategoryRepository,
        IMapper<BLL.DTO.CouponCategory, App.DTO.CouponCategory> couponCategoryMapper) : base(repository, mapper)
    {
        TicketInOrderMapper = ticketInOrderMapper;
        TicketInOrderRepository = ticketInOrderRepository;
        ItemInOrderMapper = itemInOrderMapper;
        ItemInOrderRepository = itemInOrderRepository;
        UserCouponRepository = userCouponRepository;
        UserCouponMapper = userCouponMapper;
        UserCategoryRepository = userCategoryRepository;
        UserCategoryMapper = userCategoryMapper;
        CouponCategoryMapper = couponCategoryMapper;
        CouponCategoryRepository = couponCategoryRepository;
    }
    
    public async Task<App.BLL.DTO.Order> ProceedOrderConfirmation(App.BLL.DTO.Order order, Guid userId)
    {
        var orderToUpdate = await Repository.FirstOrDefaultAsync(order.Id);
        var userCoupon = await FindCouponByOrderId(order.Id);
        order.Discount = userCoupon == null ? 0 : userCoupon.CouponCategory!.Discount;
        order.OrderNr = orderToUpdate!.OrderNr;
        order.AppUserId = orderToUpdate.AppUserId;
        order.Price = await CalculatePrice(order.Id);
        await UpdateUserPrivilegeStatus(order.Id, await GetOrdersAmount(userId));
   
        order.FinalPrice = order.Price *(userCoupon == null?  1:1-userCoupon.CouponCategory!.Discount/100);
        order.InProcess = false;
        
        return Update(order,userId);
    }
    
    
    public async Task<int> GetOrdersAmount(Guid userId)
    {
        return await Repository.GetOrdersAmount(userId);
    }
    

    public async Task<App.BLL.DTO.Order> GetCurrentOrderByUserIdAsync(Guid userId, bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetCurrentOrderByUserIdAsync(userId))!;
    }

    public async Task<IEnumerable<App.BLL.DTO.Order>> GetAllOrdersByUserId(Guid userId)
    {
        return (await Repository.GetAllOrdersByUserId(userId)).Select(x=>Mapper.Map(x)!);
    }

    private  async Task<App.BLL.DTO.UserCoupon?> FindCouponByOrderId(Guid orderId)
    {
       return await new UserCouponService(UserCouponRepository,UserCouponMapper).GetUserCouponByOrderId(orderId);
    }

    private async Task<decimal> CalculatePrice(Guid orderId)
    {
        return await new TicketInOrderService(TicketInOrderRepository, TicketInOrderMapper)
                   .CalculateTicketsInOrderPrice(orderId)
               + await new ItemInOrderService(ItemInOrderRepository, ItemInOrderMapper)
                   .CalculateItemsInOrderPrice(orderId);
    }

    private  async Task UpdateUserPrivilegeStatus(Guid userId,int ordersAmount)
    {
        await UpdateUserCategory(userId, ordersAmount);
        await UpdateUserCoupons(userId, ordersAmount);
    }
    private  async Task UpdateUserCategory(Guid userId,int ordersAmount)
    {
        await new UserCategoryService(UserCategoryRepository,UserCategoryMapper)
            .SetUserCategoryByOrdersAmount(userId, ordersAmount);
    }

    private  async Task UpdateUserCoupons(Guid userId,int ordersAmount)
    {
        await new CouponCategoryService(CouponCategoryRepository,CouponCategoryMapper)
            .SetUserCouponsByOrdersAmount(userId, ordersAmount);
    }
}