using System;
using System.Linq;
using System.Threading.Tasks;
using App.BLL;
using App.BLL.DTO;
using App.Contracts.BLL;
using App.DAL.EF;
using App.DTO.Identity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;
using Card = App.DTO.Card;
using ComponentTranslation = App.DTO.ComponentTranslation;
using Coordinate = App.DTO.Coordinate;
using CoordinateLocation = App.DTO.CoordinateLocation;
using CouponCategory = App.DTO.CouponCategory;
using ItemCategory = App.DTO.ItemCategory;
using ItemInOrder = App.DTO.ItemInOrder;
using MenuItem = App.DTO.MenuItem;
using Order = App.DTO.Order;
using TicketInOrder = App.DTO.TicketInOrder;
using UserCategory = App.DTO.UserCategory;
using UserCoupon = App.DTO.UserCoupon;
using UserInCategory = App.DTO.UserInCategory;
using UserLog = App.DTO.UserLog;

namespace Testing.WebApp;

public class OrderServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly AppDbContext _ctx;
    private readonly IAppBLL _bll;

    public OrderServiceTests( ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();
        _ctx.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        _ctx.ChangeTracker.AutoDetectChangesEnabled = false;

        var dalMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AppUser, App.Domain.Identity.AppUser>().ReverseMap();
            cfg.CreateMap<Card, App.Domain.Card>().ReverseMap();
            cfg.CreateMap<Coordinate, App.Domain.Coordinate>().ReverseMap();
            cfg.CreateMap<ComponentTranslation, App.Domain.ComponentTranslation>().ReverseMap();
            cfg.CreateMap<CouponCategory, App.Domain.CouponCategory>().ReverseMap();
            cfg.CreateMap<ItemCategory, App.Domain.ItemCategory>().ReverseMap();
            cfg.CreateMap<ItemInOrder, App.Domain.ItemInOrder>().ReverseMap();
            cfg.CreateMap<MenuItem, App.Domain.MenuItem>().ReverseMap();
            cfg.CreateMap<Order, App.Domain.Order>().ReverseMap();
            cfg.CreateMap<App.DTO.Ticket, App.Domain.Ticket>().ReverseMap();
            cfg.CreateMap<TicketInOrder, App.Domain.TicketInOrder>().ReverseMap();
            cfg.CreateMap<UserCategory, App.Domain.UserCategory>().ReverseMap();
            cfg.CreateMap<UserCoupon, App.Domain.UserCoupon>().ReverseMap();
            cfg.CreateMap<UserInCategory, App.Domain.UserInCategory>().ReverseMap();
            cfg.CreateMap<UserLog, App.Domain.UserLog>().ReverseMap();
            cfg.CreateMap<CoordinateLocation, App.Domain.CoordinateLocation>().ReverseMap();
        });
        var bllMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<App.BLL.DTO.Identity.AppUser, AppUser>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.Card, Card>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.Coordinate, Coordinate>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.ComponentTranslation, ComponentTranslation>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.CouponCategory, CouponCategory>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.ItemCategory, ItemCategory>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.ItemInOrder, ItemInOrder>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.MenuItem, MenuItem>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.Order, Order>().ReverseMap();
            cfg.CreateMap<Ticket, App.DTO.Ticket>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.TicketInOrder, TicketInOrder>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.UserCategory, UserCategory>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.UserCoupon, UserCoupon>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.UserInCategory, UserInCategory>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.UserLog, UserLog>().ReverseMap();
            cfg.CreateMap<App.BLL.DTO.CoordinateLocation, CoordinateLocation>().ReverseMap();
        });


        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());


        _bll = new AppBLL(new AppUOW(_ctx, new Mapper(dalMapper)), new Mapper(bllMapper));
    }

    //Testing BaseEntityService methods
    [Fact]
    public void Test_baseBehaviour_sync()
    {
        
        //Add new ticket, test for uniqueness
        var fakeUserId = Guid.NewGuid();
        Assert.NotEqual(Guid.Empty, fakeUserId);
        var ticket = new Ticket()
        {
            Name = "TestTicket",
            Price = 3,
            DayRange = 25
        };
        var addedTicket = _bll.Tickets.Add(ticket, fakeUserId);
        CheckTicket(ticket, addedTicket, false, false, true);
        _bll.SaveChanges();
        _ctx.ChangeTracker.Clear();
        
        
        //Find previously added ticket by Id, test that are equal
        var foundTicket = _bll.Tickets.FirstOrDefault(addedTicket.Id);
        var foundTicketCopy = new Ticket()
        {
            Name = foundTicket!.Name,
            Price = foundTicket!.Price,
            DayRange = foundTicket.DayRange,
            Id = foundTicket.Id,
            CreatedAt = foundTicket.CreatedAt,
            CreatedBy = foundTicket.CreatedBy
        };
        CheckTicket(addedTicket, foundTicket!, false, false, false);
        
        
        //Update the previously found ticket
        foundTicket!.Name = "NewName";
        foundTicket.Price = 4;
        foundTicket.DayRange = 28;
        var updatedTicket = _bll.Tickets.Update(foundTicket, fakeUserId);
        var unsavedUpdatedTicket = _bll.Tickets.FirstOrDefault(updatedTicket.Id);
            //Test previously found and unsaved are equal
        CheckTicket(foundTicketCopy, unsavedUpdatedTicket!, false, false, false);
        _bll.SaveChanges();
        _ctx.ChangeTracker.Clear();
            //Test that updated ticket is actually updated
        var savedUpdatedTicket = _bll.Tickets.FirstOrDefault(updatedTicket.Id);
        Assert.True(_bll.Tickets.Exists(updatedTicket.Id));
        CheckTicket(updatedTicket, savedUpdatedTicket!, true, false, false);

        
        //Add new ticket, test that is added
        var ticket2 = new Ticket()
        {
            Name = "TestTicket2",
            Price = 3,
            DayRange = 25
        };
        var updatedTicket2 = _bll.Tickets.Add(ticket2, fakeUserId);
        _bll.SaveChanges();
        _ctx.ChangeTracker.Clear();
        Assert.True(_bll.Tickets.Exists(updatedTicket2.Id));
            //Test that we have 2 added tickets
        var twoTickets = _bll.Tickets.GetAll();
        Assert.NotNull(twoTickets);
        Assert.Equal(2, twoTickets.ToList().Count);

        
        //Remove the ticket
        var removedTicket = _bll.Tickets.Remove(savedUpdatedTicket!, fakeUserId);
            //Test if before saving it exists
        Assert.True(_bll.Tickets.Exists(updatedTicket.Id));
        _bll.SaveChanges();
        _ctx.ChangeTracker.Clear();
            //Does not exist after saving
        Assert.False(_bll.Tickets.Exists(updatedTicket.Id));
        Assert.Null(_bll.Tickets.FirstOrDefault(updatedTicket.Id));
        CheckTicket(removedTicket, removedTicket!, true, true, false);
            //Remove the second ticket by Id, test that there is no tickets
        var removedTicket2 = _bll.Tickets.Remove(updatedTicket2.Id, fakeUserId);
        _bll.SaveChanges();
        _ctx.ChangeTracker.Clear();
        CheckTicket(removedTicket2, removedTicket2!, false, true, false);
        Assert.False(_bll.Tickets.Exists(updatedTicket2.Id));
        var zeroTickets = _bll.Tickets.GetAll();
        Assert.NotNull(zeroTickets);
        Assert.Empty(zeroTickets.ToList());
    }

    [Fact]
    public async Task Test_baseBehaviour_async()
    {
        //Add new ticket, test for uniqueness
        var fakeUserId = Guid.NewGuid();
        Assert.NotEqual(Guid.Empty, fakeUserId);
        var ticket = new Ticket()
        {
            Name = "TestTicket",
            Price = 3,
            DayRange = 25
        };
        var addedTicket = _bll.Tickets.Add(ticket, fakeUserId);
        CheckTicket(ticket, addedTicket, false, false, true);
        await _bll.SaveChangesAsync();
        _ctx.ChangeTracker.Clear();
        
        
        //Find previously added ticket by Id, test that are equal
        var foundTicket = await _bll.Tickets.FirstOrDefaultAsync(addedTicket.Id);
        var foundTicketCopy = new Ticket()
        {
            Name = foundTicket!.Name,
            Price = foundTicket!.Price,
            DayRange = foundTicket.DayRange,
            Id = foundTicket.Id,
            CreatedAt = foundTicket.CreatedAt,
            CreatedBy = foundTicket.CreatedBy
        };
        CheckTicket(addedTicket, foundTicket!, false, false, false);
        
        
        //Update the ticket
        foundTicket!.Name = "NewName";
        foundTicket.Price = 4;
        foundTicket.DayRange = 28;
        var updatedTicket = _bll.Tickets.Update(foundTicket, fakeUserId);
        var unsavedUpdatedTicket = await _bll.Tickets.FirstOrDefaultAsync(updatedTicket.Id);
            //Check that unsaved ticket is not updated
        CheckTicket(foundTicketCopy, unsavedUpdatedTicket!, false, false, false);
        await _bll.SaveChangesAsync();
        _ctx.ChangeTracker.Clear();
            //Compare saved and unsaved tickets
        var savedUpdatedTicket = await _bll.Tickets.FirstOrDefaultAsync(updatedTicket.Id);
        Assert.True(await _bll.Tickets.ExistsAsync(updatedTicket.Id));
        CheckTicket(updatedTicket, savedUpdatedTicket!, true, false, false);

        
        //Add one more ticket, check that there are two tickets
        var ticket2 = new Ticket()
        {
            Name = "TestTicket2",
            Price = 3,
            DayRange = 25
        };
        var updatedTicket2 = _bll.Tickets.Add(ticket2, fakeUserId);
        await _bll.SaveChangesAsync();
        _ctx.ChangeTracker.Clear();
        Assert.True(await _bll.Tickets.ExistsAsync(updatedTicket2.Id));
        var twoTickets = await _bll.Tickets.GetAllAsync();
        Assert.NotNull(twoTickets);
        Assert.Equal(2, twoTickets.ToList().Count);
        
        
        //Remove the ticket
        var removedTicket = await _bll.Tickets.RemoveAsync(savedUpdatedTicket!.Id, fakeUserId);
            //Before saving exists
        Assert.True(await _bll.Tickets.ExistsAsync(updatedTicket.Id));
        await _bll.SaveChangesAsync();
        _ctx.ChangeTracker.Clear();
            //After saving does not
        Assert.False(await _bll.Tickets.ExistsAsync(updatedTicket.Id));
        Assert.Null(await _bll.Tickets.FirstOrDefaultAsync(updatedTicket.Id));
        CheckTicket(removedTicket, removedTicket!, true, true, false);
            //Remove second ticket
        var removedTicket2 = await _bll.Tickets.RemoveAsync(updatedTicket2.Id, fakeUserId);
        await _bll.SaveChangesAsync();
        _ctx.ChangeTracker.Clear();
        CheckTicket(removedTicket2, removedTicket2!, false, true, false);
        Assert.False(await _bll.Tickets.ExistsAsync(updatedTicket2.Id));
            //All the tickets are deleted
        var zeroTickets = await _bll.Tickets.GetAllAsync();
        Assert.NotNull(zeroTickets);
        Assert.Empty(zeroTickets.ToList());
    }


    [Fact]
    public async Task Test_mainService_async()
    {
        var fakeUserId = Guid.NewGuid();
        Assert.NotEqual(Guid.Empty, fakeUserId);
        
        
        //Create new ticket
        var ticket = new Ticket()
        {
            Name = "TestTicket",
            Price = 3,
            DayRange = 30
        };
        var addedTicket = _bll.Tickets.Add(ticket, fakeUserId);
        await _bll.SaveChangesAsync();
        
        
        //Create new category for menu item
        var itemCategory = new App.BLL.DTO.ItemCategory()
        {
            Name = "TestCategory"
        };
        var addedItem = _bll.ItemCategories.Add(itemCategory, fakeUserId);
        await _bll.SaveChangesAsync();

        
        //Create new menu item
        var item = new App.BLL.DTO.MenuItem()
        {
            ItemName = "TestItem",
            Price = 5,
            Description = "Description",
            ItemCategoryId = addedItem.Id
        };
        var addedMenuItem = _bll.MenuItems.Add(item, fakeUserId);
        await _bll.SaveChangesAsync();

        
        //Check that user does not have any orders yet
        Assert.Equal(0, await _bll.Orders.GetOrdersAmount(fakeUserId));
        Assert.Empty((await _bll.Orders.GetAllOrdersByUserId(fakeUserId)).ToList());

        
        //Create new order for user, check if exists
        var order = await _bll.Orders.GetCurrentOrderByUserIdAsync(fakeUserId);
        await _bll.SaveChangesAsync();
        Assert.NotNull(order);
        Assert.Equal(1, order.OrderNr);
        Assert.NotEqual(Guid.Empty, order.Id);
        Assert.Equal(fakeUserId, order.AppUserId);
        Assert.True(order.InProcess);
        Assert.Equal(1, await _bll.Orders.GetOrdersAmount(fakeUserId));
            //No completed orders
        Assert.Empty((await _bll.Orders.GetAllOrdersByUserId(fakeUserId)).ToList());
        var sameOrder = await _bll.Orders.GetCurrentOrderByUserIdAsync(fakeUserId);
        Assert.Equal(order.Id,sameOrder.Id);
        Assert.Equal(1, await _bll.Orders.GetOrdersAmount(fakeUserId));
        Assert.Empty((await _bll.Orders.GetAllOrdersByUserId(fakeUserId)).ToList());
        await _bll.SaveChangesAsync();
        
        
        //Add ticket and menu item to the order
        await _bll.TicketsInOrder.AddTicketInCurrentOrderAsync(fakeUserId, addedTicket.Id);
        await _bll.SaveChangesAsync();
        await _bll.ItemsInOrder.AddItemInCurrentOrderAsync(fakeUserId,addedMenuItem.Id,10);
        await _bll.SaveChangesAsync();
        _ctx.ChangeTracker.Clear();
        
        
        //Proceed Order Confirmation
        var updatedOrder = await _bll.Orders.GetCurrentOrderByUserIdAsync(fakeUserId);
        var proceededOrder = await _bll.Orders.ProceedOrderConfirmation(updatedOrder, fakeUserId);
        await _bll.SaveChangesAsync();
        _ctx.ChangeTracker.Clear();

        Assert.Equal(1, await _bll.Orders.GetOrdersAmount(fakeUserId));
        Assert.Single((await _bll.Orders.GetAllOrdersByUserId(fakeUserId)).ToList());
        Assert.NotNull(proceededOrder);
        Assert.NotEqual(Guid.Empty, proceededOrder.Id);
        Assert.Equal(fakeUserId, proceededOrder.AppUserId);
        Assert.False(proceededOrder.InProcess);
        Assert.Equal(53, proceededOrder.FinalPrice);
        Assert.Equal(0, proceededOrder.Discount);
        Assert.Equal(53, proceededOrder.Price);
        Assert.Equal(1, proceededOrder.OrderNr);
    }

    private static void CheckTicket(Ticket expected, Ticket actual, bool updated, bool removed,
        bool unequalIds)
    {
        Assert.NotNull(actual);
        if (unequalIds)
        {
            Assert.NotEqual(expected.Id, actual!.Id);
        }
        else
        {
            Assert.Equal(expected.Id, actual!.Id);
        }

        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Price, actual.Price);
        Assert.Equal(expected.DayRange, actual.DayRange);
        Assert.NotEqual(DateTime.MinValue, actual.CreatedAt);
        Assert.NotEqual(Guid.Empty, actual.CreatedBy);

        if (updated)
        {
            Assert.NotEqual(DateTime.MinValue, actual.UpdatedAt);
            Assert.Equal(actual.CreatedBy, actual.UpdatedBy);
            Assert.NotEqual(Guid.Empty, actual.UpdatedBy);
        }

        if (removed)
        {
            Assert.NotEqual(DateTime.MinValue, actual.DeletedAt);
            Assert.Equal(actual.CreatedBy, actual.DeletedBy);
            Assert.NotEqual(Guid.Empty, actual.DeletedBy);
        }
    }
}