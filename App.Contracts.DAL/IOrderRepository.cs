using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IOrderRepository : IEntityRepository<App.DTO.Order>, IOrderRepositoryCustom<App.DTO.Order>
{
    
}

public interface IOrderRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}