using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class UserLogRepository : BaseEntityRepository<App.DTO.UserLog, App.Domain.UserLog, AppDbContext>, IUserLogRepository
{
    public UserLogRepository(AppDbContext dbContext, IMapper<App.DTO.UserLog, App.Domain.UserLog> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.UserLog>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}