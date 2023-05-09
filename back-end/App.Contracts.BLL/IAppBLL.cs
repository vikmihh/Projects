using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBLL : IBLL
{
    ICardService Cards { get; }
    ICoordinateService Coordinates { get; }
    ICouponCategoryService CouponCategories { get; }
    IItemCategoryService ItemCategories { get; }
    IItemInOrderService ItemsInOrder { get; }
    IMenuItemService MenuItems { get; }
    IOrderService Orders { get; }
    ITicketService Tickets { get; }
    ITicketInOrderService TicketsInOrder { get; }
    IUserLogService UserLogs { get; }
    IUserCategoryService UsersCategory { get; }
    IUserCouponService UsersCoupon { get; } 
    IUserInCategoryService UsersInCategory { get; }
    IComponentTranslationService ComponentTranslations { get; }
    ICoordinateLocationService CoordinatesLocation { get; }
}