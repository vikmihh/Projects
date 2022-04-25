using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
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
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //Remove cascade delete
        foreach(var relationship in builder.Model
            .GetEntityTypes()
            .SelectMany(e=> e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}