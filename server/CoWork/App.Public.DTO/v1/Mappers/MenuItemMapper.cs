using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class MenuItemMapper : BaseMapper<Public.DTO.v1.MenuItem, App.BLL.DTO.MenuItem>
{
    public MenuItemMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.MenuItem? MapToBll(Public.DTO.v1.MenuItem? menuItem)
    {
        if (menuItem == null) return null;
        var res = new App.BLL.DTO.MenuItem()
        {
            Id = menuItem.Id,
            ItemName = menuItem.ItemName,
            Description = menuItem.Description,
            Price = menuItem.Price,
            ItemCategoryId = menuItem.ItemCategoryId
        };
        return res;
    }
    public static Public.DTO.v1.MenuItem? MapToPublic(App.BLL.DTO.MenuItem? menuItem)
    {
        if (menuItem == null) return null;
        var res = new Public.DTO.v1.MenuItem()
        {
            Id = menuItem.Id,
            ItemName = menuItem.ItemName,
            Description = menuItem.Description,
            Price = menuItem.Price,
            ItemCategoryId = menuItem.ItemCategoryId,
            ItemCategoryName = menuItem.ItemCategory == null? "" : menuItem.ItemCategory!.Name
        };
        return res;
    }
}