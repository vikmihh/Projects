using App.Domain;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IUserCategoryRepository : IEntityRepository<UserCategory>
{
    Task<IEnumerable<Card>> GetAllByFirstNameAsync(string firstName, bool noTracking = true);
}