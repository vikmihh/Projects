using App.Contracts.DAL;
using Base.DAL.EF;
using Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CouponCategoryRepository :
    BaseEntityRepository<App.DTO.CouponCategory, App.Domain.CouponCategory, AppDbContext>, ICouponCategoryRepository
{
    private readonly IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> _userCouponMapper;
    private readonly IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> _ticketInOrderMapper;
    private readonly IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> _itemInOrderMapper;
    private readonly IMapper<App.DTO.UserCategory, App.Domain.UserCategory> _userCategoryMapper;
    private readonly IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> _couponCategoryMapper;
    private readonly IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> _userInCategoryMapper;
    private readonly IMapper<App.DTO.Order, App.Domain.Order> _orderMapper;

    public CouponCategoryRepository(AppDbContext dbContext,
        IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> mapper,
        IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> userCouponMapper,
        IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> ticketInOrderMapper,
        IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> itemInOrderMapper,
        IMapper<App.DTO.UserCategory, App.Domain.UserCategory> userCategoryMapper,
        IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> couponCategoryMapper,
        IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> userInCategoryMapper,
        IMapper<App.DTO.Order, App.Domain.Order> orderMapper) : base(dbContext, mapper)
    {
        _userCouponMapper = userCouponMapper;

        _ticketInOrderMapper = ticketInOrderMapper;
        _itemInOrderMapper = itemInOrderMapper;
        _userCategoryMapper = userCategoryMapper;
        _couponCategoryMapper = couponCategoryMapper;
        _userInCategoryMapper = userInCategoryMapper;
        _orderMapper = orderMapper;
    }


    public async Task SetUserCouponsByOrdersAmount(Guid userId, int ordersAmount)
    {
        var couponCategories = (await CreateQuery().Where(uc => uc.OrdersAmount == ordersAmount).ToListAsync());
        foreach (var couponCategory in couponCategories)
        {
            await new UserCouponRepository(RepoDbContext, _userCouponMapper, _ticketInOrderMapper, _itemInOrderMapper,
                     _userCategoryMapper, _couponCategoryMapper,_userInCategoryMapper,_orderMapper)
                .CreateCouponByUserId(userId, couponCategory.Id, couponCategory.Name);
        }
    }
}