using App.Contracts.DAL;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class UserLogRepository : BaseEntityRepository<App.DTO.UserLog, App.Domain.UserLog, AppDbContext>,
    IUserLogRepository
{
    private readonly IMapper<App.DTO.TicketInOrder, App.Domain.TicketInOrder> _ticketInOrderMapper;
    private readonly IMapper<App.DTO.Order, App.Domain.Order> _orderMapper;
    private readonly IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> _itemInOrderMapper;
    private readonly IMapper<App.DTO.UserCategory, App.Domain.UserCategory> _userCategoryMapper;
    private readonly IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> _userCouponMapper;

    private readonly IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> _couponCategoryMapper;
    private readonly IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> _userInCategoryMapper;

    public UserLogRepository(AppDbContext dbContext,
        IMapper<App.DTO.UserLog, App.Domain.UserLog> mapper, IMapper<App.DTO.TicketInOrder,
            App.Domain.TicketInOrder> ticketInOrderMapper, IMapper<App.DTO.Order, App.Domain.Order> orderMapper,
        IMapper<App.DTO.ItemInOrder, App.Domain.ItemInOrder> itemInOrderMapper,
        IMapper<App.DTO.UserCategory, App.Domain.UserCategory> userCategoryMapper,
        IMapper<App.DTO.UserCoupon, App.Domain.UserCoupon> userCouponMapper,
        IMapper<App.DTO.CouponCategory, App.Domain.CouponCategory> couponCategoryMapper,
        IMapper<App.DTO.UserInCategory, App.Domain.UserInCategory> userInCategoryMapper) : base(dbContext, mapper)
    {
        _orderMapper = orderMapper;
        _ticketInOrderMapper = ticketInOrderMapper;
        _itemInOrderMapper = itemInOrderMapper;
        _userCategoryMapper = userCategoryMapper;
        _userCouponMapper = userCouponMapper;
        _couponCategoryMapper = couponCategoryMapper;
        _userInCategoryMapper = userInCategoryMapper;
    }

    public async Task<App.DTO.UserLog> RegisterEntrance(Guid ticketInOrderId, Guid userId, bool noTracking = true)
    {
        var ticketInOrderRepository = new TicketInOrderRepository(RepoDbContext, _ticketInOrderMapper, _orderMapper,
            _itemInOrderMapper, _userCategoryMapper, _userCouponMapper, _couponCategoryMapper, _userInCategoryMapper);
        if (!await ticketInOrderRepository.IsTicketValid(ticketInOrderId))
        {
            throw new NullReferenceException($"Ticket is obsolete!");
        }

        var userLog = new App.DTO.UserLog()
        {
            AppUserId = userId,
            TicketInOrderId = ticketInOrderId,
            From = DateTime.UtcNow,
            Until = DateTime.UtcNow.AddMinutes(1)
        };
        return Add(userLog, userId);
    }
}