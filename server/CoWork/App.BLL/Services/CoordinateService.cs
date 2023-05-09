using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class CoordinateService : BaseEntityService<App.BLL.DTO.Coordinate, App.DTO.Coordinate, ICoordinateRepository>, ICoordinateService
{
    public CoordinateService(ICoordinateRepository repository, IMapper<BLL.DTO.Coordinate, App.DTO.Coordinate> mapper) : base(repository, mapper)
    {
    }
    

    public async Task<IEnumerable<Coordinate>> GetCoordinatesByLocationId(Guid locationId, bool noTracking = true)
    {
        return (await Repository.GetCoordinatesByLocationId(locationId)).Select(x=>Mapper.Map(x)!);
    }

    public async Task<IEnumerable<Coordinate>> GetAvailableCoordinates()
    {
        return (await Repository.GetAvailableCoordinates()).Select(x=>Mapper.Map(x)!);
    }
}