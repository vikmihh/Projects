using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserLogRepository : IEntityRepository<UserLog>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}