using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserInCategoryMapper : BaseMapper<App.DTO.UserInCategory, Domain.UserInCategory>
{
    public UserInCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}