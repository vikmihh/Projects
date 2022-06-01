using App.BLL.Mappers;
using App.BLL.Services;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork UnitOfWork;
    private readonly AutoMapper.IMapper _mapper;
    
    public AppBLL(IAppUnitOfWork unitOfWork,  IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public override async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    private ICardService? _cards;
    public ICardService Cards => 
        _cards ??= new CardService(UnitOfWork.Cards, new CardMapper(_mapper));
    
    private ICoordinateService? _coordinates;
    public ICoordinateService Coordinates => 
        _coordinates ??= new CoordinateService(UnitOfWork.Coordinates, new CoordinateMapper(_mapper));
    
    private ICouponCategoryService? _couponCategories;
    public ICouponCategoryService CouponCategories => 
        _couponCategories ??= new CouponCategoryService(UnitOfWork.CouponCategories, new CouponCategoryMapper(_mapper));
    
    private IItemCategoryService? _itemCategories;
    public IItemCategoryService ItemCategories => 
        _itemCategories ??= new ItemCategoryService(UnitOfWork.ItemCategories, new ItemCategoryMapper(_mapper));
    
    private IItemInOrderService? _itemsInOrder;
    public IItemInOrderService ItemsInOrder => 
        _itemsInOrder ??= new ItemInOrderService(UnitOfWork.ItemsInOrder, new ItemInOrderMapper(_mapper));
    
    private IMenuItemService? _menuItems;
    public IMenuItemService MenuItems => 
        _menuItems ??= new MenuItemService(UnitOfWork.MenuItems, new MenuItemMapper(_mapper));
    
    private IOrderService? _orders;
    public IOrderService Orders => 
        _orders ??= new OrderService(UnitOfWork.Orders, new OrderMapper(_mapper));
    
    private ITicketService? _tickets;
    public ITicketService Tickets => 
        _tickets ??= new TicketService(UnitOfWork.Tickets, new TicketMapper(_mapper));
    
    private ITicketInOrderService? _ticketsInOrder;
    public ITicketInOrderService TicketsInOrder => 
        _ticketsInOrder ??= new TicketInOrderService(UnitOfWork.TicketsInOrder, new TicketInOrderMapper(_mapper));
    
    private IUserLogService? _userLogs;
    public IUserLogService UserLogs => 
        _userLogs ??= new UserLogService(UnitOfWork.UserLogs, new UserLogMapper(_mapper));
    
    private IUserCategoryService? _userCategories;
    public IUserCategoryService UsersCategory => 
        _userCategories ??= new UserCategoryService(UnitOfWork.UsersCategory, new UserCategoryMapper(_mapper));
    
    private IUserCouponService? _userCoupons;
    public IUserCouponService UsersCoupon => 
        _userCoupons ??= new UserCouponService(UnitOfWork.UsersCoupon, new UserCouponMapper(_mapper));
    
    private IUserInCategoryService? _usersInCategories;
    public IUserInCategoryService UsersInCategory => 
        _usersInCategories ??= new UserInCategoryService(UnitOfWork.UsersInCategory, new UserInCategoryMapper(_mapper));
    
    private IComponentTranslationService? _componentTranslations;
    public IComponentTranslationService ComponentTranslations => 
        _componentTranslations ??= new ComponentTranslationService(UnitOfWork.ComponentTranslations, new ComponentTranslationMapper(_mapper));
    
    
    private ICoordinateLocationService? _coordinatesLocation;
    public ICoordinateLocationService CoordinatesLocation => 
        _coordinatesLocation ??= new CoordinateLocationService(UnitOfWork.CoordinatesLocation, new CoordinateLocationMapper(_mapper));
}