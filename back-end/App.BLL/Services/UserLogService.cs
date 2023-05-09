using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class UserLogService : BaseEntityService<App.BLL.DTO.UserLog, App.DTO.UserLog, IUserLogRepository>, IUserLogService
{
    
   
    public UserLogService(IUserLogRepository repository, IMapper<App.BLL.DTO.UserLog, App.DTO.UserLog> mapper) : base(repository, mapper)
    {
       
    }

    public async Task<App.BLL.DTO.UserLog> RegisterEntrance(Guid ticketInOrderId, Guid userId, bool noTracking = true)
    {
        return Mapper.Map(await Repository.RegisterEntrance(ticketInOrderId, userId))!;
    }
}