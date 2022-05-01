using App.Contracts.DAL;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CoordinateRepository : BaseEntityRepository<Coordinate, AppDbContext>, ICoordinateRepository
{
    public CoordinateRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true)
    {
        throw new NotImplementedException();
    }
}