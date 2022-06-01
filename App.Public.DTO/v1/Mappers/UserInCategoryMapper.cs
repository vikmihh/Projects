using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.v1.Mappers;

public class UserInCategoryMapper : BaseMapper<Public.DTO.v1.UserInCategory, App.BLL.DTO.UserInCategory>
{
    public UserInCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
    public static App.BLL.DTO.UserInCategory? MapToBll(Public.DTO.v1.UserInCategory? userInCategory)
    {
        if (userInCategory == null) return null;
        var res = new App.BLL.DTO.UserInCategory()
        {
            Id = userInCategory.Id,
            AppUserId = userInCategory.AppUserId,
            UserCategoryId = userInCategory.UserCategoryId
        };
        return res;
    }
    
    public static Public.DTO.v1.UserInCategory? MapToPublic(App.BLL.DTO.UserInCategory? userInCategory)
    {
        if (userInCategory == null) return null;
        var res = new Public.DTO.v1.UserInCategory()
        {
            Id = userInCategory.Id,
            UserCategoryName = userInCategory.UserCategory!.CategoryName
        };
        return res;
    }
}