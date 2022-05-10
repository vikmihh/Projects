using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserLogRepository : IEntityRepository<App.DTO.UserLog>, IUserLogRepositoryCustom<App.DTO.UserLog>
{
    
}

public interface IUserLogRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}