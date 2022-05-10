using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICoordinateService : IEntityService<App.BLL.DTO.Coordinate>, ICoordinateRepositoryCustom<App.BLL.DTO.Coordinate>
{
    
}