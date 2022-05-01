using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    ICardRepository Cards { get; }
    ICoordinateRepository Coordinates { get; }
    ICouponCategoryRepository CouponCategories { get; }
    IItemCategoryRepository ItemCategories { get; }
    IItemInOrderRepository ItemsInOrder { get; }
    IMenuItemRepository MenuItems { get; }
    IOrderRepository Orders { get; }
    ITicketRepository Tickets { get; }
    ITicketInOrderRepository TicketsInOrder { get; }
    IUserLogRepository UserLogs { get; }
    IUserCategoryRepository UsersCategory { get; }
    IUserCouponRepository UsersCoupon { get; }
    IUserInCategoryRepository UsersInCategory { get; }
}