using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserCategoryMapper : BaseMapper<App.DTO.UserCategory, Domain.UserCategory>
{
    public UserCategoryMapper(IMapper mapper) : base(mapper)
    {
    }
}