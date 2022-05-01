using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IMenuItemRepository : IEntityRepository<MenuItem>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}