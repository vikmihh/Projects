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


    public async Task<decimal> CalculateItemsInOrderPrice(Guid orderId)
    {
       return await Repository.CalculateItemsInOrderPrice(orderId);
    }

    public async Task<App.BLL.DTO.ItemInOrder> RemoveItemInOrderAsync(Guid itemInOrderId, Guid userId, int amount)
    {
        return Mapper.Map(await Repository.RemoveItemInOrderAsync(itemInOrderId, userId, amount))!;
    }

    public async Task<App.BLL.DTO.ItemInOrder> AddItemInCurrentOrderAsync(Guid userId, Guid menuItemId, int amount)
    {
        return Mapper.Map(await Repository.AddItemInCurrentOrderAsync(userId, menuItemId, amount))!;
    }

    public async Task<IEnumerable<App.BLL.DTO.ItemInOrder>> GetItemsInOrderByOrderId(Guid orderId, bool noTracking = true)
    {
        return (await Repository.GetItemsInOrderByOrderId(orderId)).Select(x => Mapper.Map(x))!;
    }
}