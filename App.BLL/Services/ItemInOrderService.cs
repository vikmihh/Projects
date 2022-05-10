using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class ItemInOrderService : BaseEntityService<App.BLL.DTO.ItemInOrder, App.DTO.ItemInOrder, IItemInOrderRepository>, IItemInOrderService
{
    public ItemInOrderService(IItemInOrderRepository repository, IMapper<BLL.DTO.ItemInOrder, App.DTO.ItemInOrder> mapper) : base(repository, mapper)
    {
    }

    public Task<IEnumerable<ItemInOrder>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}