using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class ItemInOrderMapper : BaseMapper<Public.DTO.v1.ItemInOrder, App.BLL.DTO.ItemInOrder>
{
    public ItemInOrderMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.ItemInOrder? MapToBll(Public.DTO.v1.ItemInOrder? itemInOrder)
    {
        if (itemInOrder == null) return null;
        var res = new App.BLL.DTO.ItemInOrder()
        {
            Id = itemInOrder.Id,
            Amount = itemInOrder.Amount,
            MenuItemId = itemInOrder.MenuItemId,
            AppUserId = itemInOrder.AppUserId,
            OrderId = itemInOrder.OrderId
        };
        return res;
    }
    
    public static Public.DTO.v1.ItemInOrder? MapToPublic(App.BLL.DTO.ItemInOrder? itemInOrder)
    {
        if (itemInOrder == null) return null;
        var res = new Public.DTO.v1.ItemInOrder()
        {
         
            Id = itemInOrder.Id,
            Amount = itemInOrder.Amount,
            MenuItemId = itemInOrder.MenuItemId,
            OrderId = itemInOrder.OrderId,
            Description =itemInOrder.MenuItem==null?"": itemInOrder.MenuItem!.Description,
            ItemCategoryName = "",
            ItemName = itemInOrder.MenuItem==null?"":itemInOrder.MenuItem.ItemName,
            Price = itemInOrder.MenuItem==null?0:itemInOrder.MenuItem!.Price
        };
        return res;
    }
}