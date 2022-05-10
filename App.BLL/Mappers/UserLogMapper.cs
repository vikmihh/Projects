using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserLogMapper : BaseMapper<App.BLL.DTO.UserLog, App.DTO.UserLog>
{
    public UserLogMapper(IMapper mapper) : base(mapper)
    {
    }
}