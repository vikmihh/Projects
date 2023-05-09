using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICoordinateLocationService : IEntityService<App.BLL.DTO.CoordinateLocation>, ICoordinateLocationRepositoryCustom<App.BLL.DTO.CoordinateLocation>
{
    
}