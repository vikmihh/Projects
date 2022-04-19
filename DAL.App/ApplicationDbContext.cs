using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Card> Cards { get; set; } = default!;
    public DbSet<Coordinate> Coordinates { get; set; } = default!;
    public DbSet<CouponCategory> CouponCategories { get; set; } = default!;
    public DbSet<ItemCategory> ItemCategories { get; set; } = default!;
    public DbSet<ItemInOrder> ItemsInOrder { get; set; } = default!;
    public DbSet<MenuItem> MenuItems { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<Ticket> Tickets { get; set; } = default!;
    public DbSet<TicketInOrder> TicketsInOrders { get; set; } = default!;
    public DbSet<UserCategory> UserCategories { get; set; } = default!;
    public DbSet<UserCoupon> UserCoupons { get; set; } = default!;
    public DbSet<UserInCategory> UsersInCategories { get; set; } = default!;
    public DbSet<UserLog> UserLogs { get; set; } = default!;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}