using App.Contracts.DAL;
using App.DTO.Identity;
using Base.DAL.EF;
using Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class OrderRepository : BaseEntityRepository<App.DTO.Order, App.Domain.Order, AppDbContext>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext, IMapper<App.DTO.Order, App.Domain.Order> mapper
    ) : base(dbContext, mapper)
    {
    }

    public async Task<int> GetOrdersAmount(Guid appUserId) =>
        await CreateQuery().Where(a => a.AppUserId.Equals(appUserId)).CountAsync();


    public async Task<IEnumerable<App.DTO.Order>> GetAllOrdersByUserId(Guid userId)
    {
        return (await CreateQuery().Where(o => o.AppUserId.Equals(userId) && o.InProcess == false).Include("Card")
            .ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public async Task<App.DTO.Order> GetCurrentOrderByUserIdAsync(Guid userId, bool noTracking = true)
    {
        var currentOrder = await CreateQuery(noTracking)
            .Include("ItemsInOrder")
            .Where(a => a.AppUserId.Equals(userId) && a.InProcess)
            .FirstOrDefaultAsync();
        if (currentOrder != null) return Mapper.Map(currentOrder)!;
        return Add(new App.DTO.Order
        {
            AppUserId = userId,
            InProcess = true,
            OrderNr = await GetOrdersAmount(userId) + 1
        }, userId);
    }
}