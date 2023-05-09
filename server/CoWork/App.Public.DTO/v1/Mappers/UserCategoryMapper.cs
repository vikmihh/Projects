using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserCategoryMapper : BaseMapper<Public.DTO.v1.UserCategory, App.BLL.DTO.UserCategory>
{
    public UserCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.UserCategory? MapToBll(Public.DTO.v1.UserCategory? userCategory)
    {
        if (userCategory == null) return null;
        var res = new App.BLL.DTO.UserCategory()
        {
            Id = userCategory.Id,
            CategoryName = userCategory.CategoryName,
            OrdersAmount = userCategory.OrdersAmount
        };
        return res;
    }
    
    public static Public.DTO.v1.UserCategory? MapToPublic(App.BLL.DTO.UserCategory? userCategory)
    {
        if (userCategory == null) return null;
        var res = new Public.DTO.v1.UserCategory()
        {
            Id = userCategory.Id,
            CategoryName = userCategory.CategoryName,
            OrdersAmount = userCategory.OrdersAmount
        };
        return res;
    }
}