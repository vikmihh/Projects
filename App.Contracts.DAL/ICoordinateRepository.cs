using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ICoordinateRepository : IEntityRepository<Coordinate>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}