using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class UserLogRepository : BaseEntityRepository<UserLog, AppDbContext>, IUserLogRepository
{
    public UserLogRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}