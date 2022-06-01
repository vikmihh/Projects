using App.Contracts.DAL;
using Base.DAL.EF;
using Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserCouponRepository : BaseEntityRepository<App.DTO.UserCoupon, App.Domain.UserCoupon, AppDbContext>,
    IUserCouponRepository
{
    private readonly IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> _ticketInOrderMapper;
    private readonly IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> _itemInOrderMapper;
    private readonly IMapper<App.DTO.UserCategory, App.Domain.UserCategory> _userCategoryMapper;
    private readonly IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> _couponCategoryMapper;
    private readonly IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> _userInCategoryMapper;
    private readonly IMapper<App.DTO.Order, App.Domain.Order> _orderMapper;

    public UserCouponRepository(AppDbContext dbContext, IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> mapper,
        IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> ticketInOrderMapper,
        IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> itemInOrderMapper,
        IMapper<App.DTO.UserCategory, App.Domain.UserCategory> userCategoryMapper,
        IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> couponCategoryMapper,
        IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> userInCategoryMapper,
        IMapper<App.DTO.Order, App.Domain.Order> orderMapper) : base(dbContext, mapper)
    {
        _ticketInOrderMapper = ticketInOrderMapper;
        _itemInOrderMapper = itemInOrderMapper;
        _userCategoryMapper = userCategoryMapper;
        _couponCategoryMapper = couponCategoryMapper;
        _userInCategoryMapper = userInCategoryMapper;
        _orderMapper = orderMapper;
    }

    public async Task<int> GetUserCouponsAmount(Guid appUserId, Guid couponCategoryId) => await CreateQuery()
        .Where(a => a.AppUserId.Equals(appUserId) && a.CouponCategoryId == couponCategoryId).CountAsync();

    public async Task<App.DTO.UserCoupon> CreateCouponByUserId(Guid userId, Guid couponCategoryId,
        string couponCategoryName)
    {
        var newCoupon = new App.DTO.UserCoupon()
        {
            AppUserId = userId,
            CouponCategoryId = couponCategoryId,
            PromoCode = couponCategoryName + ((await GetUserCouponsAmount(userId, couponCategoryId)) + 1)
        };
        var addedCoupon = Add(newCoupon, userId);
        await RepoDbContext.SaveChangesAsync();
        return addedCoupon;
    }

    public async Task<IEnumerable<App.DTO.UserCoupon>> GetAvailableUserCouponsByUserId(Guid userId)
    {
        return (await CreateQuery().Where(uc => uc.AppUserId.Equals(userId) && uc.OrderId == null).Include("CouponCategory").ToListAsync())
            .Select(x => Mapper.Map(x)!);
    }

    public async Task<App.DTO.UserCoupon?> GetUserCouponByOrderId(Guid orderId,
        bool noTracking = true)
    {
        var userCoupon = (await CreateQuery(noTracking).Include("CouponCategory")
            .Where(userCoupon => userCoupon.DeletedAt == null &&
                                 userCoupon.OrderId.Equals(orderId)).FirstOrDefaultAsync());

        return userCoupon != null ? Mapper.Map(userCoupon)! : null;
    }

    public async Task ActivateUserCouponByPromoCode(Guid userId, string promoCode, bool isAdding)
    {
        var order = await new OrderRepository(RepoDbContext, _orderMapper, _ticketInOrderMapper, _itemInOrderMapper,
                Mapper, _userCategoryMapper, _couponCategoryMapper, _userInCategoryMapper)
            .GetCurrentOrderByUserIdAsync(userId);
        var userCoupon = await CreateQuery().Where(userCoupon =>
            userCoupon.AppUserId.Equals(userId) && userCoupon.PromoCode.Equals(promoCode)).FirstOrDefaultAsync();
        if (userCoupon != null)
        {
            userCoupon.OrderId = isAdding ? order.Id : null;
            Update(Mapper.Map(userCoupon)!, userId);
        }
    }
}