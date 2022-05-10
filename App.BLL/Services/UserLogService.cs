using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class UserLogService : BaseEntityService<App.BLL.DTO.UserLog, App.DTO.UserLog, IUserLogRepository>, IUserLogService
{
    public UserLogService(IUserLogRepository repository, IMapper<BLL.DTO.UserLog, App.DTO.UserLog> mapper) : base(repository, mapper)
    {
    }

    public Task<IEnumerable<UserLog>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}