using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.DAL.EF.Repositories;
using Base.Contracts.DAL;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUnitOfWork
{
    private readonly AutoMapper.IMapper _mapper;

    public AppUOW(AppDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private ICardRepository? _cards;

    public virtual ICardRepository Cards =>
        _cards ??= new CardRepository(UOWDbContext, new CardMapper(_mapper));

    private ICoordinateRepository? _coordinates;

    public virtual ICoordinateRepository Coordinates =>
        _coordinates ??= new CoordinateRepository(UOWDbContext, new CoordinateMapper(_mapper));

    private ICouponCategoryRepository? _couponCategories;

    public virtual ICouponCategoryRepository CouponCategories =>
        _couponCategories ??= new CouponCategoryRepository(UOWDbContext, new CouponCategoryMapper(_mapper),
            new UserCouponMapper(_mapper), new TicketInOrderMapper(_mapper), new ItemInOrderMapper(_mapper),
            new UserCategoryMapper(_mapper), new CouponCategoryMapper(_mapper),new UserInCategoryMapper(_mapper),new OrderMapper(_mapper));

    private IItemCategoryRepository? _itemCategories;

    public virtual IItemCategoryRepository ItemCategories =>
        _itemCategories ??= new ItemCategoryRepository(UOWDbContext, new ItemCategoryMapper(_mapper));

    private IItemInOrderRepository? _itemsInOrder;

    public virtual IItemInOrderRepository ItemsInOrder =>
        _itemsInOrder ??= new ItemInOrderRepository(UOWDbContext, new ItemInOrderMapper(_mapper),
            new OrderMapper(_mapper), new TicketInOrderMapper(_mapper), new UserCategoryMapper(_mapper),
            new UserCouponMapper(_mapper), new CouponCategoryMapper(_mapper), new UserInCategoryMapper(_mapper));

    private IMenuItemRepository? _menuItems;

    public virtual IMenuItemRepository MenuItems =>
        _menuItems ??= new MenuItemRepository(UOWDbContext, new MenuItemMapper(_mapper));

    private IOrderRepository? _orders;

    public virtual IOrderRepository Orders =>
        _orders ??= new OrderRepository(UOWDbContext, new OrderMapper(_mapper));

    private ITicketInOrderRepository? _ticketsInOrder;

    public virtual ITicketInOrderRepository TicketsInOrder =>
        _ticketsInOrder ??=
            new TicketInOrderRepository(UOWDbContext, new TicketInOrderMapper(_mapper), new OrderMapper(_mapper),
                new ItemInOrderMapper(_mapper), new UserCategoryMapper(_mapper), new UserCouponMapper(_mapper),
                new CouponCategoryMapper(_mapper), new UserInCategoryMapper(_mapper));

    private ITicketRepository? _tickets;

    public virtual ITicketRepository Tickets =>
        _tickets ??= new TicketRepository(UOWDbContext, new TicketMapper(_mapper));

    private IUserCategoryRepository? _userCategories;

    public virtual IUserCategoryRepository UsersCategory =>
        _userCategories ??= new UserCategoryRepository(UOWDbContext, new UserCategoryMapper(_mapper),
            new UserInCategoryMapper(_mapper));

    private IUserCouponRepository? _userCoupons;

    public virtual IUserCouponRepository UsersCoupon =>
        _userCoupons ??= new UserCouponRepository(UOWDbContext, new UserCouponMapper(_mapper), new TicketInOrderMapper(_mapper), new ItemInOrderMapper(_mapper),
            new UserCategoryMapper(_mapper), new CouponCategoryMapper(_mapper),new UserInCategoryMapper(_mapper),new OrderMapper(_mapper));

    private IUserInCategoryRepository? _usersInCategories;

    public virtual IUserInCategoryRepository UsersInCategory =>
        _usersInCategories ??= new UserInCategoryRepository(UOWDbContext, new UserInCategoryMapper(_mapper));


    private IUserLogRepository? _userLogs;

    public virtual IUserLogRepository UserLogs =>
        _userLogs ??= new UserLogRepository(UOWDbContext, new UserLogMapper(_mapper), new TicketInOrderMapper(_mapper),
            new OrderMapper(_mapper), new ItemInOrderMapper(_mapper), new UserCategoryMapper(_mapper),
            new UserCouponMapper(_mapper), new CouponCategoryMapper(_mapper), new UserInCategoryMapper(_mapper));


    private IComponentTranslationRepository? _componentTranslations;

    public virtual IComponentTranslationRepository ComponentTranslations =>
        _componentTranslations ??=
            new ComponentTranslationRepository(UOWDbContext, new ComponentTranslationMapper(_mapper));


    private ICoordinateLocationRepository? _coordinatesLocation;

    public virtual ICoordinateLocationRepository CoordinatesLocation =>
        _coordinatesLocation ??= new CoordinateLocationRepository(UOWDbContext, new CoordinateLocationMapper(_mapper));
}