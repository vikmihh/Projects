using App.Contracts.DAL;
using App.DTO;
using Base.DAL.EF;
using Base.Contracts;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CoordinateRepository : BaseEntityRepository<App.DTO.Coordinate, App.Domain.Coordinate, AppDbContext>,
    ICoordinateRepository
{
    public CoordinateRepository(AppDbContext dbContext, IMapper<App.DTO.Coordinate, App.Domain.Coordinate> mapper) :
        base(dbContext, mapper)
    {
    }

    public async Task<IEnumerable<App.DTO.Coordinate>> GetCoordinatesByLocationId(Guid locationId,
        bool noTracking = true)
    {
        return (await CreateQuery(noTracking)
                .Where(location => location.CoordinateLocationId.CompareTo(locationId) == 0).ToListAsync())
            .Select(x => Mapper.Map(x)!);
    }

    public async Task<IEnumerable<App.DTO.Coordinate>> GetAvailableCoordinates()
    {
        return (await CreateQuery().Where(e => e.DeletedAt == null).Include("CoordinateLocation").ToListAsync()).Select(x => Mapper.Map(x)!);
    }
}