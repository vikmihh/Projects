using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICoordinateLocationRepository : IEntityRepository<App.DTO.CoordinateLocation>, ICoordinateLocationRepositoryCustom<App.DTO.CoordinateLocation>
{
    
}

public interface ICoordinateLocationRepositoryCustom<TEntity>
{
    
}