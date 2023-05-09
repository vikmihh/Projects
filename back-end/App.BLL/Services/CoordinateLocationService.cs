using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class CoordinateLocationService : BaseEntityService<App.BLL.DTO.CoordinateLocation, App.DTO.CoordinateLocation, ICoordinateLocationRepository>, ICoordinateLocationService
{
    public CoordinateLocationService(ICoordinateLocationRepository repository, IMapper<CoordinateLocation, App.DTO.CoordinateLocation> mapper) : base(repository, mapper)
    {
    }

   
}