
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICoordinateRepository : IEntityRepository<App.DTO.Coordinate>, ICoordinateRepositoryCustom<App.DTO.Coordinate>
{
   
}

public interface ICoordinateRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}