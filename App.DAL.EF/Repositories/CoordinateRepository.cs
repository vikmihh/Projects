using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.DAL.EF;
using Base.Contracts;

namespace App.DAL.EF.Repositories;

public class CoordinateRepository : BaseEntityRepository<App.DTO.Coordinate, App.Domain.Coordinate, AppDbContext>, ICoordinateRepository
{
    public CoordinateRepository(AppDbContext dbContext, IMapper<App.DTO.Coordinate, App.Domain.Coordinate> mapper) : base(dbContext, mapper)
    {
    }

    public Task<IEnumerable<App.DTO.Coordinate>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}