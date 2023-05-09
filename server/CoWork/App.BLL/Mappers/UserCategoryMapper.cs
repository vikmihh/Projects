using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserCategoryMapper : BaseMapper<App.BLL.DTO.UserCategory, App.DTO.UserCategory>
{
    public UserCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}