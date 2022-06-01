using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class ItemCategoryMapper : BaseMapper<Public.DTO.v1.ItemCategory, App.BLL.DTO.ItemCategory>
{
    public ItemCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.ItemCategory? MapToBll(Public.DTO.v1.ItemCategory? itemCategory)
    {
        if (itemCategory == null) return null;
        var res = new App.BLL.DTO.ItemCategory()
        {
            Id = itemCategory.Id,
            Name = itemCategory.Name
        };
        return res;
    }
    
    public static Public.DTO.v1.ItemCategory? MapToPublic(App.BLL.DTO.ItemCategory? itemCategory)
    {
        if (itemCategory == null) return null;
        var res = new Public.DTO.v1.ItemCategory()
        {
            Id = itemCategory.Id,
            Name = itemCategory.Name
        };
        return res;
    }
}