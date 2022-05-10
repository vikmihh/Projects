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

    public Task<IEnumerable<Order>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}