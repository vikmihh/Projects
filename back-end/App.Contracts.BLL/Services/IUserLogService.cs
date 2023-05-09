using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IUserLogService : IEntityService<App.BLL.DTO.UserLog>, IUserLogRepositoryCustom<App.BLL.DTO.UserLog>
{
    
}