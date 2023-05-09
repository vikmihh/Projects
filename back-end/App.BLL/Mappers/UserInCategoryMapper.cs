using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserInCategoryMapper : BaseMapper<App.BLL.DTO.UserInCategory, App.DTO.UserInCategory>
{
    public UserInCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}