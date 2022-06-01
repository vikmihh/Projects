using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class OrderService : BaseEntityService<App.BLL.DTO.Order, App.DTO.Order, IOrderRepository>, IOrderService
{
    public OrderService(IOrderRepository repository, IMapper<BLL.DTO.Order, App.DTO.Order> mapper) : base(repository, mapper)
    {
    }
    
    public async Task<App.BLL.DTO.Order> ProceedOrderConfirmation(App.BLL.DTO.Order order, Guid userId)
    {
        return Mapper.Map( await Repository.ProceedOrderConfirmation(Mapper.Map(order)!, userId))!;
    }
    

    public async Task<int> GetOrdersAmount(Guid userId)
    {
        return await Repository.GetOrdersAmount(userId);
    }
    

    public async Task<App.BLL.DTO.Order> GetCurrentOrderByUserIdAsync(Guid userId, bool noTracking = true)
    {
        return Mapper.Map(await Repository.GetCurrentOrderByUserIdAsync(userId))!;
    }

    public async Task<IEnumerable<App.BLL.DTO.Order>> GetAllOrdersByUserId(Guid userId)
    {
        return (await Repository.GetAllOrdersByUserId(userId)).Select(x=>Mapper.Map(x)!);
    }
}