using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserLogRepository : IEntityRepository<App.DTO.UserLog>, IUserLogRepositoryCustom<App.DTO.UserLog>
{
    
}

public interface IUserLogRepositoryCustom<TEntity>
{
    // //Here must be transferred userId and ticketId and returned full instance of UserLog
    Task<TEntity> RegisterEntrance(Guid ticketInOrderId, Guid userId, bool noTracking = true);

}