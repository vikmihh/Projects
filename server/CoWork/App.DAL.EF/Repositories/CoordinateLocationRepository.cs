using App.Contracts.DAL;
using App.DTO;
using Base.Contracts;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CoordinateLocationRepository : BaseEntityRepository<App.DTO.CoordinateLocation, App.Domain.CoordinateLocation, AppDbContext>, ICoordinateLocationRepository
{
    public CoordinateLocationRepository(AppDbContext dbContext, IMapper<CoordinateLocation, Domain.CoordinateLocation> mapper) : base(dbContext, mapper)
    {
    }
    
}