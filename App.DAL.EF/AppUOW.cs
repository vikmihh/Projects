using App.Contracts.DAL;
using App.DAL.EF.Repositories;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork
{
    public AppUOW(AppDbContext dbContext) : base(dbContext)
    {
        
    }

    private ICardRepository? _cards;
    public virtual ICardRepository Cards =>
        _cards ??= new CardRepository(UOWDbContext);
    
    private ICoordinateRepository? _coordinates;
    public virtual ICoordinateRepository Coordinates =>
        _coordinates ??= new CoordinateRepository(UOWDbContext);
    
    private ICouponCategoryRepository? _couponCategories;
    public virtual ICouponCategoryRepository CouponCategories =>
        _couponCategories ??= new CouponCategoryRepository(UOWDbContext);
    
    private IItemCategoryRepository? _itemCategories;
    public virtual IItemCategoryRepository ItemCategories =>
        _itemCategories ??= new ItemCategoryRepository(UOWDbContext);
    
    private IItemInOrderRepository? _itemsInOrder;
    public virtual IItemInOrderRepository ItemsInOrder =>
        _itemsInOrder ??= new ItemInOrderRepository(UOWDbContext);
    
    private IMenuItemRepository? _menuItems;
    public virtual IMenuItemRepository MenuItems =>
        _menuItems ??= new MenuItemRepository(UOWDbContext);
    
    private IOrderRepository? _orders;
    public virtual IOrderRepository Orders =>
        _orders ??= new OrderRepository(UOWDbContext);
    
    private ITicketInOrderRepository? _ticketsInOrder;
    public virtual ITicketInOrderRepository TicketsInOrder =>
        _ticketsInOrder ??= new TicketInOrderRepository(UOWDbContext);
    
    private ITicketRepository? _tickets;
    public virtual ITicketRepository Tickets =>
        _tickets ??= new TicketRepository(UOWDbContext);
    
    private IUserCategoryRepository? _userCategories;
    public virtual IUserCategoryRepository UsersCategory =>
        _userCategories ??= new UserCategoryRepository(UOWDbContext);
    
    private IUserCouponRepository? _userCoupons;
    public virtual IUserCouponRepository UsersCoupon =>
        _userCoupons ??= new UserCouponRepository(UOWDbContext);
    
    private IUserInCategoryRepository? _usersInCategories;
    public virtual IUserInCategoryRepository UsersInCategory =>
        _usersInCategories ??= new UserInCategoryRepository(UOWDbContext);
    
    private IUserLogRepository? _userLogs;
    public virtual IUserLogRepository UserLogs =>
        _userLogs ??= new UserLogRepository(UOWDbContext);
}