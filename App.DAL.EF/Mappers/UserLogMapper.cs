using App.DTO;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserLogMapper : BaseMapper<App.DTO.UserLog, Domain.UserLog>
{
    public UserLogMapper(IMapper mapper) : base(mapper)
    {
    }
}