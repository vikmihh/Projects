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

    public Task<IEnumerable<Coordinate>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}